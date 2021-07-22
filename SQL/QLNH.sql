create database QLBanQuanAo
GO

use QLBanQuanAo
GO


Create Table Admin
(
	UserAdmin VARCHAR(30) PRIMARY KEY,
	PassAdmin VARCHAR(30) NOT NULL,
	Hoten nVARCHAR(30)
)
GO
INSERT INTO Admin(UserAdmin,PassAdmin,Hoten)VALUES('Kizzz','291120',N'Kiệt')

CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1) PRIMARY KEY,
	HoTen nVarchar(50) NOT NULL,
	Taikhoan Varchar(50) UNIQUE,
	Matkhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiachiKH nVarchar(200),
	DienthoaiKH Varchar(50),	
	Ngaysinh DATETIME
)
GO

Create Table Loaisanpham
(
	MaloaiSP int Identity(1,1) PRIMARY KEY,
	Tensanpham nvarchar(50) NOT NULL
)
GO

CREATE TABLE NCC
(
	MaNCC INT IDENTITY(1,1) PRIMARY KEY,
	TenNCC NVARCHAR(50) NOT NULL,
	Diachi NVARCHAR(100),
	Dienthoai VARCHAR(50)
)
GO

CREATE TABLE SANPHAM
(
	MaSP INT IDENTITY(1,1) PRIMARY KEY,
	TenSP NVARCHAR(100) NOT NULL,
	Giaban Decimal(18,0) CHECK (Giaban>=0),
	Mota NVarchar(Max),
	Anhbia NVARCHAR(50),
	Ngaycapnhat DATETIME,
	Soluongton INT,
	MaloaiSP INT,
	MaNCC INT,
	Constraint FK_Loaisanpham Foreign Key(MaloaiSP) References Loaisanpham(MaloaiSP),
	Constraint FK_NCC Foreign KEY (MaNCC) References NCC(MaNCC)
)
GO

--CREATE TABLE SIZE (
--	MaSize INT  PRIMARY KEY,
--	TenSize NVARCHAR(50)
--)
--go

--CREATE TABLE Color (
--	MaColor INT  PRIMARY KEY,
--	TenColor NVARCHAR(50)
--)
--go

--CREATE TABLE CHITIETSANPHAM(
--	MaSP int  ,
--	MaSize int ,
--	MaColor int,
--	Soluong int,
--	Constraint FK_Masanpham Foreign Key(MaSP) References SANPHAM (MaSP),
--	Constraint FK_Size Foreign Key(MaSize) References SIZE (MaSize),
--	Constraint PK_CTSP PRIMARY KEY(MaSP,MaSize,MaColor)
--)
--GO

CREATE TABLE DONDATHANG
(
	MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
	Dathanhtoan bit,
	Tinhtranggiaohang  bit,
	Ngaydat Datetime,
	Ngaygiao Datetime,	
	MaKH INT,
	CONSTRAINT FK_Khachhang FOREIGN KEY (MaKH) REFERENCES Khachhang(MaKH)
)
Go
 CREATE TABLE CHITIETDONTHANG
(
	MaDonHang INT,
	MaSP INT,
	--MaSize INT NULL,
	--MaColor int NULL,
	Soluong Int Check(Soluong>0),
	Dongia Decimal(18,0) Check(Dongia>=0),	
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDonHang,MaSP),
	CONSTRAINT FK_Donhang FOREIGN KEY (Madonhang) REFERENCES Dondathang(Madonhang),
	CONSTRAINT FK_MASP FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
	--CONSTRAINT FK_MASP FOREIGN KEY (MaSP,MaSize,MaColor) REFERENCES CHITIETSANPHAM(MaSP,MaSize,MaColor)
)



INSERT INTO Loaisanpham(Tensanpham)VALUES(N'Quần')
INSERT INTO Loaisanpham(Tensanpham)VALUES(N'Áo')
INSERT INTO Loaisanpham(Tensanpham)VALUES(N'Đồng Hồ')
INSERT INTO Loaisanpham(Tensanpham)VALUES(N'Giày')
INSERT INTO Loaisanpham(Tensanpham)VALUES(N'Nón')
GO
INSERT INTO NCC(TenNCC,	Diachi,	Dienthoai)VALUES(N'BaoHanStore',N'Ràng','1250336879')
INSERT INTO NCC(TenNCC,	Diachi,	Dienthoai)VALUES(N'Navidas',N'Ràng','1250336879')
INSERT INTO NCC(TenNCC,	Diachi,	Dienthoai)VALUES(N'Nai',N'Ràng','1250336879')
INSERT INTO NCC(TenNCC,	Diachi,	Dienthoai)VALUES(N'Black Pink',N'Ràng','1250336879')
GO

--------------------------------Giày
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Giày Nai',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nai.png','06/25/2021',30,4,3)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Giày Nai',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nai.png','06/25/2021',30,4,3)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Giày Nai',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nai.png','06/25/2021',30,4,3)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Giày Navidas',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Giày navidas.jpg','06/25/2021',30,4,2)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Giày Navidas',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Giày navidas.jpg','06/25/2021',30,4,2)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Giày Navidas',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Giày navidas.jpg','06/25/2021',30,4,2)
----------------------------------Áo 
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Áo Sói Cô Đơn',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Áo Sói Cô Đơn.jpg','06/25/2021',30,2,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Áo Sói Cô Đơn',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Áo Sói Cô Đơn.jpg','06/25/2021',30,2,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Áo Sói Cô Đơn',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Áo Sói Cô Đơn.jpg','06/25/2021',30,2,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Áo Black vine',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Áo Black vine.jpg','06/25/2021',30,2,3)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Áo Black vine',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Áo Black vine.jpg','06/25/2021',30,2,3)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Áo Black vine',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Áo Black vine.jpg','06/25/2021',30,2,3)
----------------------------------Nón
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Nón Đen Cá Tính',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nón Đen Cá Tính.jpg','06/25/2021',30,5,4)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Nón Đen Mạnh Mẽ',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nón Đen Mạnh Mẽ.jpg','06/25/2021',30,5,4)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Nón Trắng Mộng Mơ',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nón Trắng Mộng Mơ.jpg','06/25/2021',30,5,4)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Nón vàng Nhạt xinh đẹp',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Nón vàng Nhạt xinh đẹp.jpg','06/25/2021',30,5,4)

----------------------------------Đồng Hồ
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Đồng Hồ Bạc',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Đồng Hồ Bạc.jpg','06/25/2021',30,3,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Đồng Hồ Bạc',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Đồng Hồ Bạc.jpg','06/25/2021',30,3,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Đồng Hồ Bạc',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Đồng Hồ Bạc.jpg','06/25/2021',30,3,1)
----------------------------------Quần
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Quần Jogger',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Quần Jogger.png','06/25/2021',30,1,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Quần Jogger',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Quần Jogger.png','06/25/2021',30,1,1)
INSERT INTO SANPHAM(TenSP,Giaban,Mota,Anhbia,Ngaycapnhat,Soluongton,MaloaiSP,MaNCC)VALUES(N'Quần Jogger',99000,N'Đây Là 1 Đồi Giày Đẹp Và Rất Được Ưa chuộng Vì Tính Năng Ưu Việt Của Nó, Cụ Thể Là Tính Năng Gì Thì Shop Không Biết ',
N'Quần Jogger.png','06/25/2021',30,1,1)