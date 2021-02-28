# ContactDirectoryManagement
*Application for maintaining contact information.*

## Functionality:
- List contacts
- Add a contact
- Edit contact
- Delete/Inactivate a contact

## Project Structure:
1. ContactsDirectory
	- Configuration folder: Contains database configuration for Entities.
	- Context folder: Contains database context class.
	- Enums folder: Contains Enums.
	- Repositories folder:Contains Interfaces for defining Database operations on Entities & also contains implentation of Interfaces for Database operations on Entities.
	- Migrations Folder : Contains database migrations.
2. DataAccessLayer
	- Classes used for business model , ContactInformation contains fields required for database.
3. ContactsDirectory.Web
	- Controllers folder: MVC for consuming API's and displaying Views.
	- Views folder: Contains Views.

## Run the Application:
###### IDE used: Visual Studio 2019
Replace the connection string in ***appsettings.json*** in ***ContactsDirectory*** project, the application is configured to use SQL server database, in-case of any other Database please update the Entity Framework provider.
``` 
"ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-LKBKVVV;Database=ContactsData;Trusted_Connection=True"
  }
  ```

The application has been configured  to run both ***Contacts Directory*** and ***ContactsDirectory.Web*** as startup projects.

``` 
"ApiUrl": {
    "DefaultUrl": "http://localhost:64053/

  }
  ```
  Once the above configurations are done, Build and Run the application.
  To access the contact directory , Select Contact Directory option in navigation menu.



