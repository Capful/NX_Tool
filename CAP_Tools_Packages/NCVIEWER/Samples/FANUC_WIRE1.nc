%
(WIRE=FANUC)
(INIT=X0.Y0.)
O1000
G90G40
G90G00G54X0.Y0.
G90
(工程1)
(MAT:SKD-11 HEIGHT:30.)
(PG=30. UV=0. W-TOP=30. W-BOTTOM=0.)
(切り残し加工)
(パンチ)
G90G00X-20.Y35.
(CutNo=1)
M60
M83
M80
S923
G01X-23.F2.
S1051
G41D1X-30.F3.5
G52T0.Y10.
T3.X-75.
G02X-80.Y15.J5.
G01Y20.
X-95.
G02X-100.Y25.J5.
G01Y45.
G02X-95.Y50.I5.
G01X-80.
Y55.
G02X-75.Y60.I5.
G01X-30.
T0.Y40.
G40G50X-27.
S923
G42D2G01X-30.F2.
S1052
G52T0.Y60.
G51T3.X-75.
G03X-80.Y55.J-5.
G01Y50.
X-95.
G03X-100.Y45.J-5.
G01Y25.
G03X-95.Y20.I5.
G01X-80.
Y15.
G03X-75.Y10.I5.
G01X-30.
T0.Y35.
G40G50X-27.
M43
M40
M50
(切り離し加工)
(パンチ)
(仕上げ加工)
(パンチ)
M43
G90G00X-20.Y35.
M00
(CutNo=1)
M60
M83
M80
S923
G01X-23.F2.
S1051
G42D1X-30.F3.5
Y40.
G40X-27.
M43
M40
M50
M01
M02
%
