%
(WIRE=FANUC)
(INIT=X0.Y0.)
O1000
G90G40
G90G00G54X0.Y0.
G90
(工程1)
(MAT:SKD-11 HEIGHT:50.)
(PG=50. UV=0. W-TOP=50. W-BOTTOM=0.)
(切り残し加工)
(オープン)
G90G00X-10.Y40.
(CutNo=1)
M60
M83
M80
S924
G42D1G51T5.G01X0.Y50.F0.9
S1071
X40.F2.G60R2.K2.
T0.Y22.
G03X42.Y20.I2.
G01X43.
G02X45.Y18.J-2.
G01Y10.G60R2.K2.
T10.X55.G60R2.K2.
T0.Y18.
G02X57.Y20.I2.
G01X58.
G03X60.Y22.J2.
G01Y50.G60R2.K2.
T5.X100.
G40G50X110.Y40.
M43
M40
M50
(切り離し加工)
(オープン)
(仕上げ加工)
(オープン)
M02
%
