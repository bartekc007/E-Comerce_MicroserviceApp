create table Coupon(
   Id SERIAL primary key not null,
   ProductName varchar(24) not null,
   Description text,
   Amount int
);