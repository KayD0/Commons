using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.DataUtil.UtilXmlModel
{
    //以下の構造
    //<User>
    //  <Details>
    //    <ID>1020</ID>
    //    <Name>ABCD</Name>
    //    <City>NY</City>
    //    <Country>USA</Country>
    //    <TestList Test1="1" Test2="2" Test3="3" Test4="4"/>
    //  </Details>
    //  <Details>
    //    <ID>1020</ID>
    //    <Name>ABCD</Name>
    //    <City>NY</City>
    //    <Country>USA</Country>
    //    <TestList Test1="1" Test2="2" Test3="3" Test4="4"/>
    //  </Details>
    //</User>
    [System.Xml.Serialization.XmlRoot("User")]
    public class Users 
    {
        [System.Xml.Serialization.XmlElement("Details")]
        public List<User> users = new List<User>();
    }

    public class User
    {
        [System.Xml.Serialization.XmlElement("ID")]
        public string ID { get; set; }

        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("City")]
        public string City { get; set; }

        [System.Xml.Serialization.XmlElement("Country")]
        public string Country { get; set; }
        
        [System.Xml.Serialization.XmlElement("TestList")]
        public TestList TestList { get; set; }
    }

    public class TestList
    {
        [System.Xml.Serialization.XmlAttribute("Test1")]
        public string Test1 { get; set; }

        [System.Xml.Serialization.XmlAttribute("Test2")]
        public string Test2 { get; set; }

        [System.Xml.Serialization.XmlAttribute("Test3")]
        public string Test3 { get; set; }

        [System.Xml.Serialization.XmlAttribute("Test4")]
        public string Test4 { get; set; }
    }


}
