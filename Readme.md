### DB Structure ###

```mermaid
erDiagram
          USER {
              int user_id
              nvarchar fullname
              nvarchar avatar
              nvarchar email
              nvarchar password
              nvarchar address
              nvarchar phone
              nvarchar role
          }
          ORDER {
              int order_id
              int user_id 
              int discount_id
              int status
              smalldatetime create_at
          }
          ORDER_DETAIL {
              int order_id
              int product_id
              int quantity
              int price
          }
          DISCOUNT {
              int discount_id
              smalldatetime start_at
              smalldatetime end_at
              nvarchar code
              int discount_percent
          }
          CATEGORY {
              int category_id
              nvarchar title
              nvarchar slug
          }
          MANUFACTURE {
              int manufacture_id
              nvarchar title
              nvarchar slug
          }
          PRODUCT {
              int product_id
              text image
              nvarchar name
              int price
              int quantity
              text description
              nvarchar slug
              int category_id
              int manufacture_id
          }
          RECEIVED_NOTE {
              int received_note_id
              int user_id
              smalldatetime create_at
              smalldatetime update_at
          }
          RECEIVED_NOTE_DETAIL {
              int received_note_id
              int product_id
              int quantity
              int price
          }
          SUPPLIER {
              int supplier_id
              nvarchar name
          }
          USER ||--o{ ORDER : places
          DISCOUNT ||--o{ ORDER : has
          ORDER ||--|{ ORDER_DETAIL : includes
          PRODUCT ||--o{ ORDER_DETAIL : "ordered in"
          CATEGORY ||--|{ PRODUCT : contains
          MANUFACTURE ||--|{ PRODUCT : contains
          RECEIVED_NOTE }|--|| SUPPLIER : provides
          RECEIVED_NOTE }|--|| USER: has
          RECEIVED_NOTE ||--|{ RECEIVED_NOTE_DETAIL : contains
          RECEIVED_NOTE_DETAIL ||--|{ PRODUCT : has

```

----
### How to run ###
**Start MS SQL server from docker compose**
```
docker-compose up -d
```
> Using any SQL client to import sql file : BlazorEC.sql

**Start Application**
```
cd BlazorAppEC/Server && dotnet watch run
```
-----
### Connection String ###
```
Data Source=localhost,1433;Initial Catalog=BlazorEC;User ID=SA;Password=Tuan2903
-----
server=localhost,1433; database=BlazorEC;User ID=sa; Password=Tuan2903;Trusted_Connection=False;
```

### Scaffold Command For EF ###

```
dotnet ef dbcontext scaffold -o Models -f -d "Data Source=localhost,1433;Initial Catalog=BlazorEC;User ID=SA;Password=Tuan2903" "Microsoft.EntityFrameworkCore.SqlServer"
```
