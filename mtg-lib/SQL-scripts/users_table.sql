create table users
(
    id       serial
        constraint users_pk
            primary key,
    email    varchar(255) not null,
    password varchar(255) not null
);

alter table users
    owner to postgres;

create unique index users_email_uindex
    on users (email);

