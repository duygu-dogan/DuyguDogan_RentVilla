## Rent-Villa
 This is a rental villa website project developed with ASP.NET Core Web API, MVC, and React.Js.
 
## Language and Technologies Used
- C#, JavaScript, CSS, HTML
- **API Project:** AutoMapper, Azure.Storage.Blob, FluentValidation, Iyzipay, MediatR, Jwt Authentication, ASP.Net Entity Framework, EF Identity, SignalR, Serilog, NCronTab
- **MVC Project:** IdentityModel.Tokens, Cookie-Based Authentication, IdentityModel.Logging, Chosen, OwlCarousel, AspNetCoreHero.ToastNotification, JQuery, AJAX, Bootstrap
- **React.Js Project:** CoreUI, SignalR, Material UI, Axios, Bootstrap, React-Paginate, MDBReact, React-Router-Dom, React-Toastify, Js-Cookie
- **Database:** There are two versions of the project, SQLite and PostgreSQL. In the SQLite version (main), there is a possibility of some bugs caused by type conversions. The PostgreSQL (current) version can be found under the ‘Scheduled’ branch.
  
## Project Features
- You can list related products by selecting the region, property, and date range on the homepage, or by selecting the regions directly.
- Similarly, you can list related products from the ‘Popular Regions’ and ‘Popular Products’ menus.
- You can view the details of the product you are interested in, add products to your cart by selecting the date range and how many people will stay, make payment, track your order, and view the details.
- If you are not a user, you can create a new registration, if you are a registered user, you can log in to your account with your username or email and complete the reservation-payment transactions.
- If you have an administrator role, you can perform the following operations via the admin panel:
    - You can manage transactions related to products, features and bookings (deleting, adding, updating products/features/photos).
    - You can view notifications that a new reservation or product has been added from the message box.
    - You can list active/passive bookings. The status of the bookings in the project is checked and automatically updated every night at 00:00 via a scheduled task. However, you can manually update their status under the reservations heading or cancel them completely.
    - You can edit the authorizations of users and assign roles to endpoints.
      
## Login information
To review the project, you can create a new registration or log in with the following information. In order to access the admin panel, you need to log in with the user who has the following admin role:

- Admin    --> **Email:** exampleadmin@gmail.com **Password:** Sample1234+
- User     --> **Email:** exampleuser@gmail.com **Password:** Sample1234+

## Homepage

![RentVilla-HP1](https://github.com/duygu-dogan/DuyguDogan_RentVilla/assets/136385140/3eb61211-b26e-4cf9-9913-dcff1f98f855)

![RentVilla-HP'](https://github.com/duygu-dogan/DuyguDogan_RentVilla/assets/136385140/d18e4a6b-a201-41ea-964c-e696aedcd7a0)

![RentVilla-HP3](https://github.com/duygu-dogan/DuyguDogan_RentVilla/assets/136385140/ce0acade-6bae-4caa-a10b-eb528730e6c5)

![RentVilla-HP4](https://github.com/duygu-dogan/DuyguDogan_RentVilla/assets/136385140/7d8fc49c-1173-4fb9-a793-1b122061cca0)

## Products




