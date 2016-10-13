using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace platebloomers.Pages {
    public class About : Common.Site.Page {
        public override string Content
        {
            get
            {
                var c = new Contact();
                var link = $"<a id=\"link-{c.Key}\" href=\"/{c.Path}\" data-page=\"{c.Key}\" onclick=\"return m.link(this)\">reach out</a>";
                return "<img class=\"w50 ma db\" src=\"/image/about-plate-bloomers.jpg\" srcset=\"/image/about-plate-bloomers2x.jpg 2x\" /><br />The Plate Bloomers story began at a small second-hand shop, where I stumbled upon some beautiful vintage plates. The colorful glass and ornate details reminded me of flowers, and so I decided to experiment with creating one as a decoration for my garden. I loved it so much, it soon became a hobby for me and my husband David. Searching in antique shops, flea markets and yard sales we are always excited to find hidden gems. I imagine many were kept in cupboards only being brought out for special occasions. By turning them into flowers, my goal is to highlight the special features of each plate, give them new life, and bring them out into the sunlight where they can be enjoyed every day.<br /><br /><h2>About the flowers</h2>Each flower is unique and carefully created using vintage glass plates. The stamens are made with natural stone, and Czech glass beads soldered onto copper wires. Though none of the plates are particularly rare, each was hand picked. The flowers are assembled by hand using copper fittings and nylon washers, with a many - step process we developed ourselves. The plates are not glued. I drill through each one individually (which can be nerve wracking), but we’ve managed to perfect the approach to ensure each plate survives – and stands the test of time. They need no special care and may be left outside all year round. Hail is their only known natural enemy. The backs are finished with the same attention to detail as the fronts, so they may be planted to be visible from any angle.<br /><br /><img class=\"w75 ma db\" src=\"/image/sidebyside.jpg\" srcset=\"/image/sidebyside2x.jpg 2x\" /><br />The flowers stand freely on copper stems – available in 4, 5, and 6 - foot heights – and can be cut to any size requested. We offer the copper left natural to patina on its own, or treated accelerating the process. We also include a piece of iron rebar, to anchor the stem in the ground. Everything needed for installation is included.<br /><br /><img class=\"w50 ma db\" src=\"/image/copper-pipe-patina.jpg\" srcset=\"/image/copper-pipe-patina2x.jpg 2x\" /><br />Each Plate Bloomer is signed, numbered, and dated.<h2>About the artists, Janis and David Hancock</h2><section class=\"db ma w75 tac\"><img src=\"/image/drilling.jpg\" srcset=\"/image/drilling2x.jpg 2x\" /><img src=\"/image/cutting.jpg\" srcset=\"/image/cutting2x.jpg 2x\" /></section><br />We are both New England natives, growing up on the North Shore of Massachusetts and have lived in Seacoast New Hampshire for over 35 years. As long as I can remember, I’ve loved crafting, quilting, needlework - and most recently, projects that make use of repurposed antiques and found objects. I’m an avid gardener and member of the Portsmouth, NH Garden Club. These flowers are my first hobby I’ve decided to turn into a business – mainly because I love making them and seeing each unique flower take shape. My husband David is a home designer/builder and avid golfer.He used his construction and carpentry skills to build the beautiful displays for our booth. It has been a lot of hard work but fun to have a project we can work on together. David is the brains behind the assembly process and also does the pipe bending and soldering.I do the designing, drilling, stem finishes and make the stamens. He and I have found an inspiring hobby in pursuit of each perfect plate – and honing our process of creating and showcasing each flower.<br /><br />Thank you for visiting, and please feel free to " +
                    link +
                    " if you have any questions about our Plate Bloomers.<br />- Janis";
            }
        }

        public override string Description
        {
            get
            {
                return "The Plate Bloomers flowers are made from reclaimed dishes in Portsmouth, New Hampshire.";
            }
        }

        public override string Header { get { return null; } }

        public override string Key { get { return "about"; } }

        public override string Path { get { return "About"; } }

        public override string Title { get { return "About - Plate Bloomers"; } }

        public override string TitleNav { get { return "About"; } }
    }
}
