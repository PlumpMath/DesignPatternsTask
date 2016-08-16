del node_log.txt
start java -jar selenium-server-standalone.jar -role hub -port 4444 -maxSession 5
start java -Dwebdriver.chrome.driver=..\packages\Selenium.WebDriver.ChromeDriver.2.22.0.0\driver\chromedriver.exe -Dwebdriver.ie.driver=..\packages\Selenium.WebDriver.IEDriver.2.53.0.0\driver\IEDriverServer.exe -jar selenium-server-standalone.jar -role node -nodeConfig nodeConfig.json -log node_log.txt
