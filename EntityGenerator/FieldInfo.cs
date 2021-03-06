﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityGenerator
{
    class FieldInfo
    {
        public FieldInfo(string jsonMemberName, JsonType type)
        {
            this.JsonMemberName = jsonMemberName;
            DefaultMemberName = StringHelper.ToTitleCase(jsonMemberName);
            this.Type = type;
        }

        public string DefaultMemberName { get; private set; }
        public string JsonMemberName { get; private set; }
        public JsonType Type { get; private set; }
    }
}
