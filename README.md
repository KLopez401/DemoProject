Clone DemoProject
1. Clone project https://github.com/KLopez401/DemoProject.git
2. Make sure you have all for project (API, Application, Domain, Infrastructure)

SetUp Database and Connection
1. Go to Infrastructure Layer Project
2. Go to DbBackup and copy bak file and paste to C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup
<img width="535" alt="image" src="https://github.com/KLopez401/DemoProject/assets/74169912/fb944e0d-b11e-4edb-9a94-97fa85418567">

4. Restore bak file to sql management studio and restore the database

<img width="365" alt="image" src="https://github.com/KLopez401/DemoProject/assets/74169912/850f61a0-ee13-44a1-a527-71c98053f73e">

5. Go back to solution and go to DemoProjectDbContext under infrastructure project
6. Change your connection string according to your information
<img width="851" alt="image" src="https://github.com/KLopez401/DemoProject/assets/74169912/1e639ad4-56ec-4596-9d78-cc1bab93217d">

7. "Server=LAPTOP-D396GR4H;Database=DemoProjectDb;user=sa;password=123456;TrustServerCertificate=True"
8. Then Go to Api Layer
9. Go to Program.cs
10. On line 21 change the connection string the same info that is in your infrastructure layer

