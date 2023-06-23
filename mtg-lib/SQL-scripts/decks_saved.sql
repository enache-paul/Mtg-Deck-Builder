create table decks_saved
(
    id      serial
        constraint decks_saved_pk
            primary key,
    user_id integer      not null
        constraint decks_saved_users_id_fk
            references users,
    name    varchar(100) not null
);

alter table decks_saved
    owner to postgres;
