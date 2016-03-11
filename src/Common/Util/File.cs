using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Util {
    public static class File {
        public static async Task<string> LoadToString(string FileName) {
            string text = null;
            var f = Application.Environment.ApplicationBasePath + "/" + FileName;
            using (var sr = new StreamReader(f)) {
                text = await sr.ReadToEndAsync();
            }
            return text.Trim();
        }
        public static async Task<byte[]> LoadToBuffer(string FileName) {
            byte[] buffer = null;
            var f = Application.Environment.ApplicationBasePath + "/" + FileName;
            using (var fs = new FileStream(f, FileMode.Open, FileAccess.Read)) {
                using (var ms = new MemoryStream()) {
                    await fs.CopyToAsync(ms);
                    buffer = ms.ToArray();
                }
            }
            return buffer;
        }
    }
}
