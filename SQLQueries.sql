create sequence teachers_tid_seq
    as integer;

alter sequence teachers_tid_seq owner to postgres;

create sequence attendance_aid_seq
    as integer;

alter sequence attendance_aid_seq owner to postgres;

create sequence grades_gid_seq
    as integer;

alter sequence grades_gid_seq owner to postgres;

create table students
(
    sid      serial
        primary key,
    name     varchar(60) not null
        unique,
    email    varchar(100)
        unique,
    password varchar(60) not null
        unique,
    group_no integer     not null,
    hobby    varchar(50) not null
);

alter table students
    owner to postgres;

create table "Teacher"
(
    tid      integer default nextval('teachers_tid_seq'::regclass) not null
        constraint teachers_pkey
            primary key,
    name     varchar(60)                                           not null
        constraint teachers_name_key
            unique,
    email    varchar(100)
        constraint teachers_email_key
            unique,
    password varchar(60)                                           not null
        constraint teachers_password_key
            unique
);

alter table "Teacher"
    owner to postgres;

alter sequence teachers_tid_seq owned by "Teacher".tid;

create table labs
(
    lid         serial
        primary key,
    lab_no      integer      not null
        unique,
    date        date         not null
        unique,
    title       varchar(50)  not null
        unique,
    description varchar(500) not null
        unique
);

alter table labs
    owner to postgres;

create table attendance
(
    att_id  integer default nextval('attendance_aid_seq'::regclass) not null
        primary key,
    lab_id  integer
        constraint attendance_lid_fkey
            references labs
            on update cascade on delete cascade,
    stud_id integer
        constraint attendance_sid_fkey
            references students
            on update cascade on delete cascade,
    present boolean
);

alter table attendance
    owner to postgres;

alter sequence attendance_aid_seq owned by attendance.att_id;

create table assignments
(
    asid     serial
        primary key,
    lid      integer
        references labs
            on update cascade on delete cascade,
    as_name  varchar(30)  not null
        unique,
    deadline date         not null,
    as_desc  varchar(300) not null
        unique
);

alter table assignments
    owner to postgres;

create table submissions
(
    sub_id   integer default nextval('grades_gid_seq'::regclass) not null
        constraint grades_pkey
            primary key,
    asign_id integer
        constraint grades_asid_fkey
            references assignments
            on update cascade on delete cascade,
    stud_id  integer
        constraint grades_sid_fkey
            references students
            on update cascade on delete cascade,
    grade    integer                                             not null
        constraint grades_grade_check
            check ((grade > 1) AND (grade < 10)),
    link     varchar(50)
);

alter table submissions
    owner to postgres;

alter sequence grades_gid_seq owned by submissions.sub_id;

create table tokens
(
    tok_id serial
        primary key,
    token  varchar not null,
    used   boolean not null
);

alter table tokens
    owner to postgres;

