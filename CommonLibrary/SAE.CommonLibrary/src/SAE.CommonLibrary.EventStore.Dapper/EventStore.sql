
--创建EventStore
create table if not exists EventStore(
id varchar(128) not null,
timestamp datetime not null,
version bigint not null,
data longtext,
primary key (id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

--创建快照
create table if not exists Snapshot(
id varchar(128) not null,
type varchar(128) not null,
version bigint not null,
data longtext not null,
primary key(id)
)engine=InnoDB default charset=utf8;


