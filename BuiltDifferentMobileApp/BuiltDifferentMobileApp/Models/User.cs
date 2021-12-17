using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public abstract class User {
        public abstract int id { get; set; }
        public abstract string name { get; set; }
        public abstract int userId { get; set; }
    }
}
