select * from dept

alter view vistadept as
select
cast(isnull(row_number() over (order by dept_no), 0) as int) as posicion
, isnull(dept.dept_no, 0) as dept_no, dept.dnombre, dept.loc from dept
go
select * from vistadept where posicion = 1
select * from vistadept where posicion >= 1 and posicion < (1 + 2)
select * from vistadept where posicion >= 1 and posicion < (1 + 3)
select * from vistadept where posicion >= 7 and posicion < (7 + 2)

insert into dept values(50, 'INFORMATICA', 'GIJON', '')
insert into dept values(60, 'I+D', 'OVIEDO', '')
insert into dept values(70, 'RRHH', 'CADIZ', '')