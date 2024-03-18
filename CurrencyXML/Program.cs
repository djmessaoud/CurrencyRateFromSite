using System.Xml;
using System.Net;


Console.WriteLine("Введите код валюты (например, USD, EUR):");
string currencyCode = Console.ReadLine().ToUpper();

// Сайт российского банка заблокирован в моем регионе, поэтому я воспользовался этим.
string url = "https://www.floatrates.com/daily/rub.xml";
// создаем объект WebClient для загрузки данных
WebClient client = new WebClient();
string xmlData = client.DownloadString(url);
// создаем объект XmlDocument для загрузки данных в формате XML
XmlDocument xmlDoc = new XmlDocument();
xmlDoc.LoadXml(xmlData);
// получаем все элементы item
XmlNodeList elements = xmlDoc.GetElementsByTagName("item");
bool currencyFound = false;

foreach (XmlNode node in elements)
{
    XmlNode codeNode = node.SelectSingleNode("targetCurrency"); // получаем элемент targetCurrency у которого значение равно коду валюты 
    if (codeNode != null && codeNode.InnerText == currencyCode) // если код валюты который ищем найден
    {
        XmlNode nameNode = node.SelectSingleNode("targetName"); // получаем элемент targetName у которого значение равно названию валюты
        XmlNode valueNode = node.SelectSingleNode("exchangeRate"); // получаем элемент exchangeRate у которого значение равно курсу валюты
        Console.WriteLine($"Валюта: {nameNode.InnerText}");
        Console.WriteLine($"курс: {valueNode.InnerText}");
        currencyFound = true;
        break; // прерываем цикл
    }
}

if (!currencyFound)
{
    Console.WriteLine("Валюта не найдена.");
}
    