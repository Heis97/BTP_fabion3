ENABLE (0, 1, 2, 3, 4, 7, 8, 9)
XGLOBAL = APOS(0)
YGLOBAL = APOS(1)

S1Diameter = 9.6
S2Diameter = 9.6
S3Diameter = 9.6

SET FPOS(2) = 0
SET FPOS(3) = 0
SET FPOS(4) = 0

PrevTool = 0

OffsetX(2) = 7 
OffsetY(2) = 7

OffsetX(3) = 10 
OffsetY(3) = 10

OffsetX(4) = 15 
OffsetY(4) = 15


%
N01 G90
N05 G88 P1 P1 P0 P10 P-0.5 P1 P0 P1 P0 P0
N06 G88 P1 P1 P0 P10 P-0.5 P2 P0 P1 P0 P0
N06 G88 P1 P1 P0 P10 P-0.5 P3 P0 P1 P0 P0
N15 M30
 