﻿using System;
using System.Xml;
using RaphaelLibrary.Code.Common;

namespace RaphaelLibrary.Code.Init.SQL
{
    public abstract class SqlElementBase : IXmlReader
    {
        public string Id { get; set; }
        public abstract bool ReadXml(XmlNode node);

        public virtual SqlElementBase Clone()
        {
            return this.MemberwiseClone() as SqlElementBase;
        }
    }
}