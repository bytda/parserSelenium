using Microsoft.Data.Sqlite;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;





SqliteConnection connection = new SqliteConnection("Data Source=/Users/alina/Desktop/ParserSelenium/ParserSelenium/parser.db");
connection.Open();

string sqlExpressionDelete = "DELETE FROM par";
SqliteCommand commandDelete = new SqliteCommand(sqlExpressionDelete, connection);
commandDelete.ExecuteNonQuery();


IWebDriver driver = new ChromeDriver();
driver.Url = @"https://www.lesegais.ru/open-area/deal"; // подключаемся к сайту
Thread.Sleep(500);

var status = driver.FindElements(By.XPath(".//div[@col-id ='sellerName']"));



for (int i = 1; i < status.Count; ) // количество строк в таблице
    {        
    try
    {
        var dealNumbers = driver.FindElements(By.XPath(".//div[@col-id ='dealNumber']")); //читаем данные с таблицы
        var sellerNames = driver.FindElements(By.XPath(".//div[@col-id ='sellerName']"));
        var sellersInns = driver.FindElements(By.XPath(".//div[@col-id ='sellerInn']"));
        var buyerNames = driver.FindElements(By.XPath(".//div[@col-id ='buyerName']"));
        var buyerInns = driver.FindElements(By.XPath(".//div[@col-id ='buyerInn']"));
        var dealDates = driver.FindElements(By.XPath(".//div[@col-id ='dealDate']"));
        var Ob = driver.FindElements(By.XPath(".//div[@col-id ='0']"));

        string sqlExpression = $@"INSERT INTO par (dealNumber, sellerName, sellerInn, buyerName, buyerInn, dealDate, Ob) VALUES ('{dealNumbers[i].Text}','{sellerNames[i].Text}','{sellersInns[i].Text}','{buyerNames[i].Text}','{buyerInns[i].Text}','{dealDates[i].Text}','{Ob[i].Text}')"; //записываем данные с сайта в базу данных
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.ExecuteNonQuery();



    }
    catch (OpenQA.Selenium.StaleElementReferenceException) // обрабатываем исключение если информация успела обновиться пока мы к ней обращаемся
    {
        var dealNumbers = driver.FindElements(By.XPath(".//div[@col-id ='dealNumber']"));
        var sellerNames = driver.FindElements(By.XPath(".//div[@col-id ='sellerName']"));
        var sellersInns = driver.FindElements(By.XPath(".//div[@col-id ='sellerInn']"));
        var buyerNames = driver.FindElements(By.XPath(".//div[@col-id ='buyerName']"));
        var buyerInns = driver.FindElements(By.XPath(".//div[@col-id ='buyerInn']"));
        var dealDates = driver.FindElements(By.XPath(".//div[@col-id ='dealDate']"));
        var Ob = driver.FindElements(By.XPath(".//div[@col-id ='0']"));

        string sqlExpression = $@"INSERT INTO par (dealNumber, sellerName, sellerInn, buyerName, buyerInn, dealDate, Ob) VALUES ('{dealNumbers[i].Text}','{sellerNames[i].Text}','{sellersInns[i].Text}','{buyerNames[i].Text}','{buyerInns[i].Text}','{dealDates[i].Text}','{Ob[i].Text}')";
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.ExecuteNonQuery();
    }
    i++;

    if (i==20) {
        driver.FindElement(By.XPath(".//div//span[@title='Следующая страница']")).Click(); // переключаем следующую страницу
        Thread.Sleep(3000);
        status = driver.FindElements(By.XPath(".//div[@col-id ='sellerName']")); 
        i = 1;
                }

    }












