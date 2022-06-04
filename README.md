# FoodOrder-WebApp
 

In order to run this application you should install Visual Studio 2022 and SQL Server managment studio.

Before running the application open your SQL Server and make sure you are connected.
Then open Visual Studio and open the FoodOrder-WebApp Solution.
 You have to modify the DefaultConnection  in  appsettings.json file  in the following manner:

"DefaultConnection": "Server=YourServerNameHere;Database=Food_Order_WebApp_Db;Trusted_Connection=True"

In the first run, the program will create the database as well as injecting an Admin user data. 

After the first run of the application, make sure to login (not register) with the following details:
email: ash.app.test274@gmail.com
password: Admin123*

after logging in you can create the Food-Menu
Food Categories
Food Type
Menu Items (There are couple of images in the wwwroot folder to your assistance)

After creating the menu, the users can order the products.

A manager have access to every page in this app as well as adding employees to the system (mangers, kitchen and frontDesk), cancel orders, change order status and refund.

kitchen worker has acces to the "Manage  Order" page and can modify orders.
Front-Desk workers has access to the "Order List" page and can modify them.
After signing up a regular user (not employee) can order and buy the products and schedule a time for a pick up.

Author: Asher Yasia
