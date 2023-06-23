create table cards_saved
(
    id      bigserial
        constraint cards_saved_pk
            primary key,
    user_id integer not null
        constraint cards_saved_users_id_fk
            references users,
    deck_id integer not null
        constraint cards_saved_decks_saved_id_fk
            references decks_saved,
    card_id bigint  not null
        constraint cards_saved_cards_id_fk
            references cards,
    count   integer default 0
);

alter table cards_saved
    owner to postgres;

