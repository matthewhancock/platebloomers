module m {    
    // window.onbeforeunload
    var beforeunload = false;
    window.onbeforeunload = function () { beforeunload = true }
    //window.onkey...
    var key = {
        ctrl: false, shift: false, change: function (e, down) {
            if (e.keyCode === 17) {
                key.ctrl = down;
            } else if (e.keyCode === 16) {
                key.shift = down;
            }
        }
    }
    window.onkeydown = function (e) { key.change(e, true) };
    window.onkeyup = function (e) { key.change(e, false) };

    // window.onpopstate
    window.onpopstate = function (event) {
        if (event.state && event.state.target === "modal") {
            State.Output(event.state);
        } else {
            // if not modal, close the modal window...
            State.Callback.Complete(function () { State.Output(event.state) })
        }
    };


    //// configuration
    //var Config = { path: "" }
    // external
    var External = { Javascript: ['//platform.twitter.com/widgets.js', '//connect.facebook.net/en_US/all.js'], Facebook: { initiated: false, applicationID: null }, Twitter: {} };

    // Constants
    var Constants = { MODAL_NAME: "vo-modal", ROOT_DIV_ID: "vo-div", HOME_LEFT_ID: "vo-left", HOME_RIGHT_ID: "vo-right", HOME_CONTAINER_ID: "vo-container" };

    // language
    var Language = { close: "Close", validation: { requiredFields: "Please fill required fields" } };

    // Util
    module Util {
        type Elements = string | string[];
        
        // creating DOM elements
        export function Create(TagName: string, CssClass: string = null): HTMLElement {
            var e = document.createElement(TagName);
            if (CssClass) {
                e.setAttribute("class", CssClass);
            }
            return e;
        }
        
        // getting DOM elements
        export function Get(ElementID: string): HTMLElement {
            return document.getElementById(ElementID);
        }

        export function Hide(IDs: Elements) {
            if (Array.isArray(IDs)) {
                for (var i = 0, l = IDs.length; i < l; i++) {
                    var e = Get(IDs[i]);
                    if (e) { e.classList.add("hide") }
                }
            } else {
                var e = Get(<string>IDs);
                if (e) { e.classList.add("hide") }
            }
        }
        export function unhide(IDs: Elements) {
            if (Array.isArray(IDs)) {
                for (var i = 0, l = IDs.length; i < l; i++) {
                    var e = Get(IDs[i]);
                    if (e) { e.classList.remove("hide") }
                }
            } else {
                var e = Get(<string>IDs);
                if (e) { e.classList.remove("hide") }
            }
        }

        // Structures
        export class Callback {
            private f: Function[] = [];
            private first: Function;

            constructor(FirstCallback: Function = null) {
                this.first = FirstCallback;
            }

            Add(CallbackFunction: Function) {
                this.f[this.f.length] = CallbackFunction;
            }
            Complete(LastCallback: Function = null) {
                if (this.first) {
                    this.first();
                }
                var len = this.f.length;
                if (len > 0) {
                    for (var i = 0; i < len; i++) {
                        if (this.f[i] != null) {
                            this.f[i]();
                        }
                    }
                }
                if (LastCallback) {
                    LastCallback();
                }
            }
        }
    }
    // Controls
    module Controls {
        var div_root = Util.Get(Constants.ROOT_DIV_ID);
        export class Modal {
            private _open = false;
            private _closed = false;

            private _title: string;
            private _innerHtml: string;
            private _width: string;
            private _height: string;

            private _bg = Util.Create("div", "vo-modal-panelwrapper");
            private _div = Util.Create("div", "vo-modal-panel");
            private _div_title = Util.Create("div", "vo-modal-panel-title");
            private _div_content = Util.Create("div", "vo-modal-panel-content");


            constructor(Width: string = null, Height: string = null) {
                this._width = Width;
                this._height = Height;
            }

            get Title() { return this._title; }
            set Title(Value) { this._title = Value; }
            get InnerHtml() { return this._innerHtml; }
            set InnerHtml(Value) { this._innerHtml = Value; }

            Close() {
                // Push State on modal close...
                State.Push(lastNonModalState);
                if (!this._closed) {
                    if (div_root.contains(this._bg)) {
                        div_root.removeChild(this._bg);
                    }
                    this._closed = true;
                    this._open = false;
                }
            }
            Open() {
                if (!this._open) {
                    // initial styling
                    this._div.setAttribute("name", Constants.MODAL_NAME);
                    if (this._width) {
                        this._div.style.width = this._width;
                    }
                    if (this._height) {
                        this._div.style.height = this._height;
                    }
                    // bind events
                    this._bg.onclick = () => { this.Close(); };
                    this._div.onclick = (e) => { e.cancelBubble = true; if (e.stopPropagation) { e.stopPropagation(); } };
                    // display
                    var t = Util.Create("span", "vo-modal-panel-title-label");
                    t.innerHTML = this._title;
                    this._div_title.appendChild(t);
                    var a = <HTMLAnchorElement>Util.Create("a", "vo-modal-panel-title-closelink");
                    a.href = "javascript:;"; a.onclick = () => { this.Close(); }; a.innerHTML = "Close"; //TODO: Add client-side language... language.close;
                    this._div_title.appendChild(a);
                    this._div.appendChild(this._div_title);
                    this._div_content.innerHTML = this._innerHtml;
                    this._div.appendChild(this._div_content);

                    this._bg.appendChild(this._div);
                    div_root.appendChild(this._bg);

                    State.Callback.Add(() => { this.CloseSystem(); });
                    this._open = true;
                    this._closed = false;
                }
            }
            // closed by system (for instance when closing modal as a result of navigation)...
            CloseSystem() {
                if (!this._closed) {
                    // No state manipulation necessary because this will be taken care of by whatever triggered this
                    if (div_root.contains(this._bg)) {
                        div_root.removeChild(this._bg);
                    }
                    this._closed = true;
                    this._open = false;
                }
            }
        }
    }
    // Validation
    module Validate {
        export function Email(EmailInput: string) {
            return /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/.test(EmailInput);
        }
    }

    // State
    module State {
        export var Callback = new Util.Callback();
        export function Push(State) {
            Output(State);
            history.pushState(State, State.title, State.href);
        }
        export function Replace(State) {
            Output(State);
            history.replaceState(State, State.title, State.href);
        }

        var nav = Util.Get('n');
        export function Output(NewState, LinkDOM: HTMLAnchorElement = null) {
            if (NewState) {
                if (NewState.title) {
                    document.title = NewState.title;
                }
                var c = Util.Get('content');
                c.innerHTML = NewState.content;
                if (NewState.key) {
                    nav.dataset['key'] = NewState.key;
                }
            }
        }
    }
    var navigating = false;
    var lastSelected;

    var lastNonModalState = { href: window.document.location.pathname, title: window.document.title, content: Util.Get('content').innerHTML }; // initialize with current state
    history.replaceState(lastNonModalState, lastNonModalState.title, lastNonModalState.href); // set value of current state for future onpopstate on the original request
    export function link(linkDOM: HTMLAnchorElement) {
        if (history.pushState) {
            if (lastSelected == null || linkDOM.id !== lastSelected.id) { // don't do anything if clicking same link...
                if (!navigating) {
                    navigating = true;

                    var r = new XMLHttpRequest();
                    r.addEventListener("load", function (response) {
                        if (r.readyState == 4) {
                            var d = JSON.parse(r.responseText);
                            var state = { href: linkDOM.href, title: d.title, hrefid: linkDOM.id, content: d.content, header: d.header, key: d.key };
                            for (var p in linkDOM.dataset) {
                                state[p] = linkDOM.dataset[p];
                            }
                            history.pushState(state, state.title, state.href);
                            State.Output(state, linkDOM);
                            navigating = false;
                        }
                    });
                    r.addEventListener("error", function (response) {
                    }, false);
                    r.open('GET', "/json/" + linkDOM.dataset["page"]);
                    r.send(null);
                }
            }
            return false;
        } else {
            return true;
        }
    }

    export function a(FormID: string, Button: HTMLButtonElement, Callback: Function) {
        Button.disabled = true;
        var btnDisabled = true;
        var f = Util.Get(FormID); var e = f.querySelectorAll('input, select, div[contenteditable], textarea'); var ok = true; var jd = {};
        // Get VerificationContext from dataset
        for (var i in f.dataset) {
            if (i !== "" && i !== "action" && i !== "token") {
                jd[i] = f.dataset[i];
            }
        }

        for (var i2 = 0, l = e.length; i2 < l; i2++) {
            var ei = <HTMLInputElement>e[i2];
            if (ei.dataset['required'] && ei.value === '') {
                ok = false;
                ei.classList.add("required");
                if (ei.onkeyup == null) {
                    ei.onkeyup = function (e) { if (btnDisabled) { Button.disabled = false; btnDisabled = false } };
                }
            } else {
                ei.classList.remove("required");
            }

            if (ei.type === 'email') {
                if (ei.value !== '' && !Validate.Email(ei.value)) {
                    ok = false;
                }
            }
            var key = ei.dataset['key'];
            if (key != null) {
                if (ei.type) {
                    if (ei.type === 'checkbox') {
                        jd[key] = ei.checked;
                    } else if (ei.type === 'radio') {
                        if (ei.checked) {
                            jd[key] = ei.value;
                        }
                    } else {
                        jd[key] = ei.value;
                    }
                } else if (ei.value) { // textarea
                    jd[key] = ei.value;
                } else if (ei.contentEditable) {
                    jd[key] = ei.innerHTML;
                }
            }
        }
        var err = Util.Get(FormID + '_error');
        if (ok) {
            err.innerHTML = "";
            // submit...
            var r = new XMLHttpRequest();
            r.addEventListener("load", function (response) {
                // console.log(this.responseText);
                var o = JSON.parse(this.responseText);
                if (o.ok) {
                    if (o.message) {
                        f.innerHTML = o.message
                    } else if (o.substitute) {
                        for (var i = 0, l = o.substitute.length; i < l; i++) {
                            var e = o.substitute[i];
                            var s = Util.Get(e.id);
                            if (s) { s.innerHTML = e.value; }
                        }
                    } else if (o.form) {
                        if (!o.formModal) {
                            var tempDiv = Util.Create('div');
                            tempDiv.innerHTML = o.form;
                            f.parentNode.replaceChild(tempDiv.firstChild, f);
                        } else {
                            var modal = new Controls.Modal();
                            modal.InnerHtml = o.form;
                            modal.Title = o.title;
                            modal.Open();
                        }
                    } else if (o.script) {
                        f.innerHTML = "";
                        var script = document.createElement('script');
                        script.text = o.script;
                        f.appendChild(script);
                    } else {
                        f.innerHTML = "Saul Good";
                    }
                    if (o.reenable) { Button.disabled = false; }
                    if (o.state) {
                        if (o.state.title) {
                            document.title = o.state.title;
                        }
                        if (o.state.url) {
                            if (o.state.mode === "replace") {
                                history.replaceState(o.state.data, o.state.title, o.state.url);
                            } else {
                                history.pushState(o.state.data, o.state.title, o.state.url);
                            }
                        }
                    }
                } else {
                    err.innerHTML = o.message ? o.message : "Unknown Error...";
                }
            }, false);
            r.addEventListener("error", function (response) {
                err.innerHTML = "Error....";
            }, false);
            r.open('POST', "/" + f.dataset['a'] + "/" + f.dataset['t']);
            r.send(JSON.stringify(jd));
        } else {
            err.innerHTML = "TODO: " + Language.validation.requiredFields;
        }
    }
    //
    export function PreviewPicture(id: string, label: string) {
        var modal = new Controls.Modal('90vw', '90vh');
        modal.InnerHtml = '<picture class="picture" style="background-image:url(\'/image/' + id + '.jpg\');" />';
        modal.Title = label;
        modal.Open();
    }
}