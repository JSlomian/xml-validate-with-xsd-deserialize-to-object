using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {

        private string xmlFileName = "dane.xml";
        private string xsdFileName = "dane.xsd";

        static void Main(string[] args)
        {
            Program p = new Program();
            if (p.Validate())
            {
                p.Deserialize();
            }
        }

        private bool Validate()
        {
            XDocument xDoc = XDocument.Load(xmlFileName);
            XmlSchemaSet Schema = new XmlSchemaSet();
            Schema.Add("http://moje.dane.com", xsdFileName);
            try
            {
                xDoc.Validate(Schema, null);
            }
            catch (XmlSchemaValidationException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("invalid");
                return false;
            }
            return true;
        }

        private void Deserialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book));
            Book book;

            using (Stream xmlReader = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read))
            {
                book = (Book)xmlSerializer.Deserialize(xmlReader);

                Console.Write(book.Title);
                Console.Write(" - ");
                Console.Write(book.Author);
                Console.Write(" - ");
                Console.Write(book.Year);
                Console.WriteLine();
            }
        }
    }
}