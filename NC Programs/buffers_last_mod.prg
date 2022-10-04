#/ Controller version = 2.30.02
#/ Date = 21.11.2016 13:52
#/ User remarks = 
#0
!#/ Controller version = 2.30
!#/ Date = 21.09.2016 11:56
!#/ User remarks = 
!#0
LOCAL INT iAxis 		            ! Определение переменной "iAxis"
LOCAL REAL Veloc					! Скорость движения


FDEF(iAxis).#RL=0       	! Отключение ошибки наезда на датчик
JOG/v (iAxis),Veloc       	! Движение в отрицательную сторону
TILL (FAULT(iAxis).#RL = 1)	! Ожидание нажатия отрицательного датчика
JOG/v (iAxis),-Veloc * 0.1 	! Движение в другую сторону
TILL (FAULT(iAxis).#RL =0) 	! Ожидание отжатия отрицательного датчика
SET FPOS(iAxis) = 0			! Обнуление координаты
PTP/rev iAxis, 0, -Veloc*0.1	! Перемещение в новую координату
FDEF(iAxis).#RL=1       	! Включение ошибки наезда на датчик

STOP    

#1
!ENABLE (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14)

!N10 G91
!N20 G00 Z1
!N30 M30

OutVar = 1

STOP

#2
%1
N10 G90
N20 G88 P-10 P0 P0 P5 P0.1 P3 P0 P1 P0 P0
N30 G88 P0 P0 P0 P5 P20 P3 P0 P1 P0 P0
N40 G88 P25 P0 P0 P5 P10 P3 P0 P1 P0 P0
N50 G88 P25 P5 P0 P5 P2 P3 P0 P1 P0 P0
N60 G88 P0 P5 P0 P5 P10 P3 P0 P1 P0 P0
N70 G88 P0 P10 P0 P5 P2 P3 P0 P1 P0 P0
N80 G88 P25 P10 P0 P5 P10 P3 P0 P1 P0 P0
N90 G88 P25 P15 P0 P5 P2 P3 P0 P1 P0 P0
N100 G88 P0 P15 P0 P5 P10 P3 P0 P1 P0 P0
N110 G88 P0 P20 P0 P5 P2 P3 P0 P1 P0 P0
N120 G88 P25 P20 P0 P5 P10 P3 P0 P1 P0 P0
N130 G88 P25 P25 P0 P5 P2 P3 P0 P1 P0 P0
N140 G88 P0 P25 P0 P5 P10 P3 P0 P1 P0 P0
N150 G88 P0 P0 P1 P20 P0 P3 P0 P1 P0 P0
N160 G88 P0 P25 P1 P5 P10 P3 P0 P1 P0 P0
N170 G88 P5 P25 P1 P5 P2 P3 P0 P1 P0 P0
N180 G88 P5 P0 P1 P5 P10 P3 P0 P1 P0 P0
N190 G88 P10 P0 P1 P5 P2 P3 P0 P1 P0 P0
N200 G88 P10 P25 P1 P5 P10 P3 P0 P1 P0 P0
N210 G88 P15 P25 P1 P5 P2 P3 P0 P1 P0 P0
N220 G88 P15 P0 P1 P5 P10 P3 P0 P1 P0 P0
N230 G88 P20 P0 P1 P5 P2 P3 P0 P1 P0 P0
N240 G88 P20 P25 P1 P5 P10 P3 P0 P1 P0 P0
N250 G88 P25 P25 P1 P5 P2 P3 P0 P1 P0 P0
N260 G88 P25 P0 P1 P5 P10 P3 P0 P1 P0 P0
N261 G88 P25 P0 P25 P5 P0 P3 P0 P1 P0 P0
N262 G00 X25 Y0 V20
%N270 G88 P-10 P0 P0 P5 P0 P3 P0 P1 P0 P0
N268 G88 P-10 P0 P1 P5 P0 P2 P0 P1 P0 P0
N270 G88 P0 P0 P1 P5 P10 P2 P0 P1 P0 P0
N280 G88 P0 P0 P2 P5 P0 P2 P0 P1 P0 P0
N290 G88 P25 P0 P2 P5 P10 P2 P0 P1 P0 P0
N300 G88 P25 P5 P2 P5 P2 P2 P0 P1 P0 P0
N310 G88 P0 P5 P2 P5 P10 P2 P0 P1 P0 P0
N320 G88 P0 P10 P2 P5 P2 P2 P0 P1 P0 P0
N330 G88 P25 P10 P2 P5 P10 P2 P0 P1 P0 P0
N340 G88 P25 P15 P2 P5 P2 P2 P0 P1 P0 P0
N350 G88 P0 P15 P2 P5 P10 P2 P0 P1 P0 P0
N360 G88 P0 P20 P2 P5 P2 P2 P0 P1 P0 P0
N370 G88 P25 P20 P2 P5 P10 P2 P0 P1 P0 P0
N380 G88 P25 P25 P2 P5 P2 P2 P0 P1 P0 P0
N390 G88 P0 P25 P2 P5 P10 P2 P0 P1 P0 P0
N400 G88 P0 P0 P3 P20 P0 P2 P0 P1 P0 P0
N410 G88 P0 P25 P3 P5 P10 P2 P0 P1 P0 P0
N420 G88 P5 P25 P3 P5 P2 P2 P0 P1 P0 P0
N430 G88 P5 P0 P3 P5 P10 P2 P0 P1 P0 P0
N440 G88 P10 P0 P3 P5 P2 P2 P0 P1 P0 P0
N450 G88 P10 P25 P3 P5 P10 P2 P0 P1 P0 P0
N460 G88 P15 P25 P3 P5 P2 P2 P0 P1 P0 P0
N470 G88 P15 P0 P3 P5 P10 P2 P0 P1 P0 P0
N480 G88 P20 P0 P3 P5 P2 P2 P0 P1 P0 P0
N490 G88 P20 P25 P3 P5 P10 P2 P0 P1 P0 P0
N500 G88 P25 P25 P3 P5 P2 P2 P0 P1 P0 P0
N510 G88 P25 P0 P3 P5 P10 P2 P0 P1 P0 P0
N512 G00 X25 Y0 U20
N520 M30




#3
!#/ Controller version = 2.30
!#/ Date = 17.10.2016 13:13
!#/ User remarks = 
!#3
! Калибровка по осям Z

LOCAL REAL DistX, DistY, DistZ, VelX, VelY, VelZ, StartX, StartY, StartZ
LOCAL INT AxisNum
LOCAL INT ROUND
LOCAL REAL DSTX, DSTY, DSTZ
LOCAL REAL VELPROMX, VELPROMY
LOCAL INT FIRST
LOCAL REAL SCALEX, SCALEY

DSTX = DistX
DSTY = DistY
DSTZ = -DistZ
ROUND = 1
VELPROMX = VelX
VELPROMY = VelY
FIRST = 1

! Движение на нуль всеми осями
PTP/ev (AxisNum), StartZ, VelZ
PTP/v (0), StartX, VELPROMX
PTP/v (1), StartY, VELPROMY

TILL ^AST(0).#MOVE
TILL ^AST(1).#MOVE

! Предварительный и уточненный поиск датчика по X
STATE10:
		PTP/rev (AxisNum), DSTZ, VelZ 
		DSTX = (-1) * DSTX
		PTP/rv (0), DSTX, VELPROMX
		TILL ^AST(0).#MOVE | IN(4).2 = 0
			IF (IN(4).2 = 0)
				IF (ROUND = 1)
					GOTO STATE20
				ELSEIF (ROUND = 2) 
					GOTO STATE22
				!ELSEIF (ROUND = 3)
				!	GOTO STATE30
				END
			END
		IF FIRST = 1
			DSTX = 2 * DSTX
			FIRST = 2
		END
		GOTO STATE10

! Подготовка к уточненному поиску по X
STATE20:
		HALT(0, AxisNum)
		PTP/rev (AxisNum), DistZ, VelZ 
		PTP/ev (0), StartX, VELPROMX
		DSTX = DistX
		VELPROMX = VelX/2
		FIRST = 1
		DSTZ = -DistZ/10
		ROUND = 2
		GOTO STATE10

! Подготовка к точному поиску по X
STATE22:
		HALT(0, AxisNum)
		PTP/ev (0), StartX, VELPROMX
		DSTX = DistX
		VELPROMX = VelX/6
		FIRST = 1
		GOTO STATE25

! Точный поиск датчика по X
STATE25:
		DSTX = (-1) * DSTX
		PTP/rv (0), DSTX, VELPROMX
		TILL ^AST(0).#MOVE | IN(4).2 = 0
		IF (IN(4).2 = 0)
			GOTO STATE30
		END
		IF (FIRST = 1)
			DSTX = 2 * DSTX
			FIRST = 2
		END
		GOTO STATE25

! Подготовка поиска датчика по Y
STATE30:
		HALT(0, AxisNum)
		!PTP/ev (AxisNum), StartZ, VelZ
		ROUND = 1
		DSTZ = -DistZ
		GOTO STATE50

! Предварительный и уточненный поиск датчика по Y
STATE40:
		PTP/rev (AxisNum), DSTZ, VelZ 
		DSTY = (-1) * DSTY
		PTP/rv (1), DSTY, VELPROMY
		TILL ^AST(1).#MOVE | IN(4).3 = 0
			IF IN(4).3 = 0
				IF (ROUND = 1)
					GOTO STATE50
				ELSEIF (ROUND = 2)
					GOTO STATE52
				END
			END
		IF (FIRST = 1)
			DSTY = 2 * DSTY
			FIRST = 2
		END
		GOTO STATE40

! Подготовка к уточненному поиску по Y
STATE50:
		HALT(1, AxisNum)
		PTP/rev (AxisNum), DistZ, VelZ
		PTP/ev (1), StartY, VELPROMY
		DSTY = DistY
		FIRST = 1
		VELPROMY = VelY/2
		DSTZ = -DistZ/10
		ROUND = 2
		GOTO STATE40
		
! Подготовка к точному поиску по Y
STATE52:
		HALT(1, AxisNum)
		PTP/ev (1), StartY, VELPROMY
		DSTY = DistY
		FIRST = 1
		VELPROMY = VelY/6
		GOTO STATE55
	
! Точный поиск датчика по Y		
STATE55:
		DSTY = (-1) * DSTY
		PTP/rv (1), DSTY, VELPROMY
		TILL ^AST(1).#MOVE | IN(4).3 = 0
		IF (IN(4).3 = 0)
			GOTO STATE60
		END
		IF (FIRST = 1)
			DSTY = 2 * DSTY
			FIRST = 2
		END
		GOTO STATE55
		
! Окончание программы
STATE60:
HALT(1, AxisNum)
!Обнуление оси
SET RPOS(AxisNum) = 0
! Сохранение смещений
OffsetX(AxisNum) = FPOS(0)
OffsetY(AxisNum) = FPOS(1)

PTP/ev (AxisNum), 30, VelZ   ! Это подъем вверх после калибровки

STOP


#4
!#/ Controller version = 2.30
!#/ Date = 22.09.2016 15:31
!#/ User remarks = 
!#4
LOCAL INT iAxis 		   					! Определение переменной "iAxis"
LOCAL REAL Veloc, VelX, VelY, VelCam		! Скорость движения
LOCAL INT j

j = 0

!LOOP 6
!	OffsetX(j) = 0
!	OffsetY(j) = 0
!	j = j + 1
!END

JERK(0)=600
JERK(1)=600
JERK(2)=3000
JERK(3)=3000
JERK(4)=3000
JERK(5)=3000
JERK(6)=3000
JERK(10)=200000
JERK(11)=200000
JERK(12)=200000

!ускорение
ACC(0)=3000
ACC(1)=3000
ACC(2)=20
ACC(3)=20
ACC(4)=20
ACC(5)=20
ACC(6)=20
ACC(10)=2000
ACC(11)=2000
ACC(12)=2000
! торможение
DEC(0)=3000
DEC(1)=3000
DEC(2)=20
DEC(3)=20
DEC(4)=20
DEC(5)=20
DEC(6)=20
DEC(10)=2000
DEC(11)=2000
DEC(12)=2000

KDEC(0)=10000
KDEC(1)=10000
KDEC(2)=100000
KDEC(3)=100000
KDEC(4)=100000
KDEC(5)=100000
KDEC(6)=100000
KDEC(7)=10000
KDEC(8)=10000
KDEC(9)=10000
KDEC(10)=100000
KDEC(11)=100000
KDEC(12)=100000


MFLAGS(0).#HOME = 0
MFLAGS(1).#HOME = 0
MFLAGS(10).#HOME = 0
MFLAGS(11).#HOME = 0
MFLAGS(12).#HOME = 0
MFLAGS(13).#HOME = 0

TILL MST(0).#ENABLED = 1
TILL MST(1).#ENABLED = 1
TILL MST(2).#ENABLED = 1
TILL MST(3).#ENABLED = 1
TILL MST(4).#ENABLED = 1
TILL MST(5).#ENABLED = 1
TILL MST(6).#ENABLED = 1
TILL MST(10).#ENABLED = 1
TILL MST(11).#ENABLED = 1
TILL MST(12).#ENABLED = 1
TILL MST(13).#ENABLED = 1

! Ось Z
	iAxis = 2
	FDEF(iAxis).#RL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),Veloc       	! Движение в отрицательную сторону
	TILL (FAULT(iAxis).#RL = 1)	! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),-Veloc * 0.1 	! Движение в другую сторону
	TILL (FAULT(iAxis).#RL =0) 	! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, -Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#RL=1       	! Включение ошибки наезда на датчик

! Ось U
	iAxis = 3
	FDEF(iAxis).#RL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),Veloc       	! Движение в отрицательную сторону
	TILL (FAULT(iAxis).#RL = 1)	! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),-Veloc * 0.1 	! Движение в другую сторону
	TILL (FAULT(iAxis).#RL =0) 	! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, -Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#RL=1       	! Включение ошибки наезда на датчик

! Ось V
	iAxis = 4
	FDEF(iAxis).#RL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),Veloc       	! Движение в отрицательную сторону
	TILL (FAULT(iAxis).#RL = 1)	! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),-Veloc * 0.1 	! Движение в другую сторону
	TILL (FAULT(iAxis).#RL =0) 	! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, -Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#RL=1       	! Включение ошибки наезда на датчик

! Ось W
	iAxis = 5
	FDEF(iAxis).#RL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),Veloc       	! Движение в отрицательную сторону
	TILL (FAULT(iAxis).#RL = 1)	! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),-Veloc * 0.1 	! Движение в другую сторону
	TILL (FAULT(iAxis).#RL =0) 	! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, -Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#RL=1       	! Включение ошибки наезда на датчик
	
! Ось A
	iAxis = 6
	FDEF(iAxis).#RL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),Veloc       	! Движение в отрицательную сторону
	TILL (FAULT(iAxis).#RL = 1)	! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),-Veloc * 0.1 	! Движение в другую сторону
	TILL (FAULT(iAxis).#RL =0) 	! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, -Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#RL=1       	! Включение ошибки наезда на датчик
	
! Ось Y
	iAxis = 1
	Veloc = VelY
	FDEF(iAxis).#LL=0       	! Отключение ошибки наезда на датчик!
	JOG/v (iAxis),-Veloc       	! Движение в отрицательную сторону
	TILL (IN(0).1 = 0) 			! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),Veloc * 0.1 	! Движение в другую сторону
	TILL (IN(0).1 = 1) 			! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#LL=1       	! Включение ошибки наезда на датчик
	YGLOBAL = 0
	MFLAGS(1).#HOME = 1

! Ось X
	iAxis = 0
	Veloc = VelX
	FDEF(iAxis).#LL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),-Veloc       	! Движение в отрицательную сторону
	TILL (IN(0).0 = 0) 			! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),Veloc * 0.1 	! Движение в другую сторону
	TILL (IN(0).0 = 1) 			! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#LL=1       	! Включение ошибки наезда на датчик
	XGLOBAL = 0
	MFLAGS(0).#HOME = 1
	
	
! Ось B
	iAxis = 10
	Veloc = VelCam
	FDEF(iAxis).#LL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),-Veloc       	! Движение в отрицательную сторону
	TILL (IN(3).0 = 1) 			! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),Veloc * 0.1 	! Движение в другую сторону
	TILL (IN(3).0 = 0) 			! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#LL=1       	! Включение ошибки наезда на датчик	
	MFLAGS(10).#HOME = 1

! Ось D
	iAxis = 11
	Veloc = VelCam * 0.7
	FDEF(iAxis).#LL=0       	! Отключение ошибки наезда на датчик
	FDEF(iAxis).#SLL=0
	JOG/v (iAxis),-Veloc       	! Движение в отрицательную сторону
	TILL (IN(3).1 = 1) 			! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),Veloc * 0.1 	! Движение в другую сторону
	TILL (IN(3).1 = 0) 			! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#LL=1       	! Включение ошибки наезда на датчик
	FDEF(iAxis).#SLL=1
	MFLAGS(11).#HOME = 1
	PTP/ev (11), 180, 10
	
! Ось C
	iAxis = 12
	Veloc = VelCam
	FDEF(iAxis).#LL=0       	! Отключение ошибки наезда на датчик
	JOG/v (iAxis),-Veloc       	! Движение в отрицательную сторону
	TILL (IN(3).2 = 1) 			! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),Veloc * 0.1 	! Движение в другую сторону
	TILL (IN(3).2 = 0) 			! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#LL=1       	! Включение ошибки наезда на датчик
	MFLAGS(12).#HOME = 1
	
! Ось ZOOM
	iAxis = 13
	Veloc = 1
	FDEF(iAxis).#LL=0       	! Отключение ошибки наезда на датчикр
	JOG/v (iAxis),-Veloc       	! Движение в отрицательную сторону
	TILL (IN(3).3 = 0) 			! Ожидание нажатия отрицательного датчика
	JOG/v (iAxis),Veloc * 0.1 	! Движение в другую сторону
	TILL (IN(3).3 = 1) 			! Ожидание отжатия отрицательного датчика
	SET FPOS(iAxis) = 0			! Обнуление координаты
	PTP/rev iAxis, 0, Veloc*0.1	! Перемещение в новую координату
	FDEF(iAxis).#LL=1       	! Включение ошибки наезда на датчик
	MFLAGS(13).#HOME = 1
	
	STOP						! Останов выполнения буффера
	

#5
!OutVar = 0
!
!ECOUT(498.0, OutVar)
!
!ECIN(498.0, InVar)
!STOP

!3 2 1 0
!0 1 1 0

!DISP APOS(0)
!DISP FPOS(0)
!DISP RPOS(0)

!STOP

#6
!ENABLE (14, 15)
!XPF2 = 0
!XPF1 = 0
!XVEL(4)=6.2
!VEL(4)= 3
!OUT4.10 = 0
!OUT4.11 = 0
!STOP

!ON (XPF1 = 1)
!	OUT(4).11 = 1
!	DCOM(14) = XPF1
!RET

!ON (XPF1 <> 1)
!	OUT(4).11 = 0
!	DCOM(14) = 0
!RET





#7
ENABLE (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14)
S1Diameter = 9.6
S2Diameter = 9.6
S3Diameter = 9.6

ZGLOBAL = 30
DISP ZGLOBAL

%1
N05 G90
N10 G00 V1
M30

#8
ENABLE (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14)

!N00 G90
!N10 G00 X30 Y90 V25 Z0 U30
!N20 M3
XGLOBAL = 146
ZGLOBAL = 30

StartTool = 4
PrevTool = 4

STOP

#9
!#/ Controller version = 2.30
!#/ Date = 15.09.2016 15:34
!#/ User remarks = 
!#9

WHILE (1)

IF (XPF1 < 0)
	DCOM(14) = ABS(XPF1)
	OUT4.10 = 1
ELSEIF (XPF1 > 0)
	DCOM(14) = ABS(XPF1)
	OUT4.10 = 0
ELSEIF (XPF1 = 0)
	DCOM(14) = 0
END


IF (XPF2 < 0)
	DCOM(15) = ABS(XPF2)
	OUT4.11 = 1
ELSEIF (XPF2 > 0)
	DCOM(15) = ABS(XPF2)
	OUT4.11 = 0
ELSEIF (XPF2 = 0)
	DCOM(15) = 0
END

ECOUT(498.0, OutVar)

END

STOP

#A
axisdef X=0,Y=1,Z=2,U=3,V=4,W=5,A=6,S1=7,S2=8,S3=9,B=10,D=11,C=12,ZOOM=13,PF1=14,PF2=15
! ______________________________________________________________
! 00 - Ось X - перемещение по координате X
! 01 - Ось Y - перемещение по координате Y
! 02 - Ось Z - перемещение по Z шприца 1
! 03 - Ось U - перемещение по Z шприца 2
! 04 - Ось V - перемещение по Z шприца 3
! 05 - Ось W - перемещение по Z распылителя
! 06 - Ось A - перемещение по Z дозатора
! 07 - Ось S1 - выдавливание/всасывание шприцом 1
! 08 - Ось S2 - выдавливание/всасывание шприцом 2
! 09 - Ось S3 - выдавливание/всасывание шприцом 3
! 10 - Ось B - перемещение боковой камеры по оси X
! 11 - Ось D - перемещение боковой камеры по оси Y
! 12 - Ось C - перемещение верхней камеры по оси X
! 13 - Ось ZOOM - приближение/удаление
! 14 - Ось PF1 - выдавливание/всасывание дозатором 1 компонента
! 15 - Ось PF2 - выдавливание/всасывание дозатором 2 компонента
!_______________________________________________________________

!axisdef x=0,y=1,z=2,t=3,a=4,b=5,c=6,d=7
!global int I(100),I0,I1,I2,I3,I4,I5,I6,I7,I8,I9,I90,I91,I92,I93,I94,I95,I96,I97,I98,I99
!global real V(100),V0,V1,V2,V3,V4,V5,V6,V7,V8,V9,V90,V91,V92,V93,V94,V95,V96,V97,V98,V99

! S1Diameter - диаметр шприца 1
! S2Diameter - диаметр шприца 2
! S3Diameter - диаметр шприца 3
! PF1Diameter - диаметр дозатора 1
! PF2Diameter - диаметр дозатора 2
GLOBAL REAL S1Diameter, S2Diameter, S3Diameter, PF1Diameter, PF2Diameter

GLOBAL REAL PFDose

GLOBAL REAL OffsetX(7), OffsetY(7)

GLOBAL INT OutVar
GLOBAL INT InVar

GLOBAL REAL XGLOBAL
GLOBAL REAL YGLOBAL
GLOBAL REAL ZGLOBAL
GLOBAL REAL XPF1
GLOBAL REAL XPF2

GLOBAL REAL REDUCER

GLOBAL INT PrevTool
GLOBAL INT StartTool

! Число ПИ
LOCAL REAL PI

LOCAL INT WORKMODE

LOCAL REAL ActX
LOCAL REAL ActY
LOCAL REAL ActZ
LOCAL REAL Xact
LOCAL REAL Yact
LOCAL REAL Zactd
LOCAL REAL Zactu
LOCAL REAL CurOffsetX
LOCAL REAL CurOffsetY
LOCAL REAL CurOffsetZ
LOCAL REAL OffsetZ

G99:
	DISP "G99"
RET

! Команда G00
G00:
	LOCAL INT i
	DISP "G00"
		WORKMODE = GGETMODAL(11, 1, 0)
		IF (WORKMODE = 90)
			!DISP "90"
			i = 2
			LOOP (9)
				IF 		(gParamAddr(i) = 88)
					!DISP "X"
					PTP/wr (0), -FPOS(0) + XGLOBAL + gParamValue(i)
				ELSEIF  (gParamAddr(i) = 89)
					!DISP "Y"
					PTP/wr (1), -FPOS(1) + YGLOBAL + gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 90)
					!DISP "Z"
					PTP/wr (2), -FPOS(2) + ZGLOBAL + gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 85)
					!DISP "U"
					PTP/wr (3), -FPOS(3) + ZGLOBAL + gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 86)
					DISP "V"
					PTP/wr (4), -FPOS(4) + ZGLOBAL + gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 87)
					!DISP "W"
					PTP/wr (5), -FPOS(5) + ZGLOBAL + gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 65)
					!DISP "A"
					PTP/wr (6), -FPOS(6) + ZGLOBAL + gParamValue(i)
				END
				i = i + 1
			END
		ELSE
			!DISP "91"
			i = 2
			LOOP (9)
				IF 		(gParamAddr(i) = 88)
					!DISP "X"
					PTP/wr (0), gParamValue(i)
				ELSEIF  (gParamAddr(i) = 89)
					!DISP "Y"
					PTP/wr (1), gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 90)
					!DISP "Z"
					PTP/wr (2), gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 85)
					!DISP "U"
					PTP/wr (3), gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 86)
					!DISP "V"
					PTP/wr (4), gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 87)
					!DISP "W"
					PTP/wr (5), gParamValue(i)
				ELSEIF 	(gParamAddr(i) = 65)
					!DISP "A"
					PTP/wr (6), gParamValue(i)
				END
				i = i + 1
			END
		END
	GO ALL
	TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(2).#MOVE & ^AST(3).#MOVE & ^AST(4).#MOVE & ^AST(5).#MOVE & ^AST(6).#MOVE
RET

! Команда G05 - одиночный сферойд
G05:
PI = 3.14159
	Xact = 0
	Yact = 0
	Zactd = 0
	WORKMODE = GGETMODAL(11, 2, 0)
	!DISP "WM = ", WORKMODE
	IF ((gParamAddr(2)=80) | (gParamAddr(3)=80) | (gParamAddr(4)=80) | (gParamAddr(5)=80) | (gParamAddr(6)=80) | (gParamAddr(7)=80) | (gParamAddr(8)=80))
			
		! Проверка на номер шприца и выполнение операций
		IF (gParamValue(8) = 1)															! Шприц 1
			
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(2)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(2)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(2)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				ActX = FPOS(0) 
				ActY = FPOS(1)
				ActZ = FPOS(2)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(8) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(8) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
				IF (ActZ > ActZ + Zactd)
					PTP/re (0, 1), Xact, Yact
					PTP/re (2), Zactd	
				ELSE
					PTP/re (2), Zactd
					PTP/re (0, 1), Xact, Yact
				END
				
			!TILL ^MST(2).#MOVE
			PTP/re (7),	gParamValue(5)/(POW(S1Diameter,2)* PI /4)						! Выдавливание 
			!TILL ^MST(7).#MOVE
			PTP/re (7), (-gParamValue(7))/(POW(S1Diameter,2)* PI /4)		! Всасывание
			!TILL ^MST(7).#MOVE
			!N20 G01 Z[-gParamValue(5)]
			PTP/re (2), gParamValue(6)												! Перемещение по оси Z S1
			!STOP
			PrevTool = 2
			!OffsetZ = FPOS(2)
			TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(2).#MOVE & ^AST(7).#MOVE
			
		ELSEIF (gParamValue(8) = 2)														! Шприц 2
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(3)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(3)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!CurOffsetZ = ZGLOBAL - FPOS(3)
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(3)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				
				PTP/re(0, 1), CurOffsetX, CurOffsetY

				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(3)
				
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(8) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(8) + 1)
						Xact = 0
						Yact = 0
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END

				IF (ActZ > ActZ + Zactd)
					PTP/re (0, 1), Xact, Yact
					PTP/re (3), Zactd	
				ELSE
					PTP/re (3), Zactd
					PTP/re (0, 1), Xact, Yact
				END

			!TILL ^MST(3).#MOVE
			PTP/re (8),	gParamValue(5)/(POW(S2Diameter,2)* PI /4)						! Выдавливание 
			!TILL ^MST(8).#MOVE
			PTP/re (8),	(- gParamValue(7))/(POW(S2Diameter,2)* PI /4)	! Всасывание
			!TILL ^MST(8).#MOVE
			PTP/re (3), gParamValue(6)												! Перемещение по оси Z S2
			!STOP
			PrevTool = 3
			!OffsetZ = FPOS(3)
			TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(3).#MOVE & ^AST(8).#MOVE
			
		ELSEIF (gParamValue(8) = 3)														! Шприц 3
			!DISP "Num 3"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(4)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(4)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!DISP "CurOffsetX = ", CurOffsetX
				!DISP "CurOffsetY = ", CurOffsetY
				!CurOffsetZ = ZGLOBAL - FPOS(4)
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(4)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(4),  CurOffsetZ				
				!TILL ^MST(0).#MOVE & ^MST(1).#MOVE & ^MST(4).#MOVE
				!PTP/rev(0, 1), CurOffsetX, CurOffsetY, gParamValue(5)
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(4)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(8) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(8) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
				IF (ActZ > ActZ + Zactd)
					PTP/re (0, 1), Xact, Yact
					PTP/re (4), Zactd	
				ELSE
					PTP/re (4), Zactd
					PTP/re (0, 1), Xact, Yact
				END
				
			!TILL ^MST(4).#MOVE
			PTP/re (9),	gParamValue(5)/(POW(S3Diameter,2)* PI /4)					! Выдавливание 
			!TILL ^MST(9).#MOVE
			PTP/re (9),	(- gParamValue(7))/(POW(S3Diameter,2)* PI /4)				! Всасывание
			!TILL ^MST(9).#MOVE
			PTP/re (4), gParamValue(6)												! Перемещение по оси Z S3
			!STOP
			PrevTool = 4
			!OffsetZ = FPOS(4)
			!TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(4).#MOVE & ^AST(9).#MOVE
			
		ELSE
			DISP "Wrong Axis"
			STOP
		END
	ELSE
		DISP "Not enough parameters"
		STOP
	END
RET

! Команда G87 - выдавливание
G87:
PI = 3.14159
	IF ((gParamAddr(2)=80) | (gParamAddr(3)=80) | (gParamAddr(4)=80))
		IF 		(gParamValue(3)=1)															! Шприц 1
			PTP/re (7), gParamValue(2)/(POW(S1Diameter,2)* PI /4)							! Выдавливание
			!STOP
		ELSEIF 	(gParamValue(3)=2)															! Шприц 2
			PTP/re (8), gParamValue(2)/(POW(S2Diameter,2)* PI /4)							! Выдавливание
			!STOP	
		ELSEIF 	(gParamValue(3)=3)															! Шприц 3
			PTP/re (9), gParamValue(2)/(POW(S3Diameter,2)* PI /4)							! Выдавливание
			!STOP
		ELSEIF 	(gParamValue(3)=5)															! Дозатор
			PTP/re (14), gParamValue(2)*gParamValue(4)/100/(POW(PF1Diameter,2)* PI /4)		! Выдавливание
			PTP/re (15), gParamValue(2)*(1-gParamValue(4)/100)/(POW(PF2Diameter,2)* PI /4)	! Выдавливание
			!STOP
		ELSE
			DISP "Wrong Axis"
			!STOP
		END
	ELSE
		DISP "Wrong Parameters"
		STOP
	END
RET

! Команда G88 - метод экструзии
G88:
PI = 3.14159
	LOCAL REAL ExtractTime 		! Время на то, чтобы выдавить порцию
	LOCAL REAL ExtractVelocity	! Скорость выдваливания
	LOCAL REAL ExtractVelocityPF
	LOCAL REAL R
	LOCAL REAL L
	LOCAL REAL Portion
	LOCAL REAL n
	LOCAL REAL a, b

	IF ((gParamAddr(2)=80) | (gParamAddr(3)=80) | (gParamAddr(4)=80) | (gParamAddr(5)=80) | (gParamAddr(6)=80) | (gParamAddr(7)=80) | (gParamAddr(8)=80) | (gParamAddr(9)=80) | (gParamAddr(10)=80) | (gParamAddr(11)=80))
		! Режим работы G-кода
		Xact = 0
		Yact = 0
		WORKMODE = GGETMODAL(11, 2, 0)

		IF (gParamValue(9) = 1)							! Линейное перемещение G1
		!DISP "Type 1"
		! Перемещение по оси X и Y с заданной скоростью
		! Проверка на номер дозатора
			IF (gParamValue(7) = 1)						! Шприц 1
			!DISP "Syringe 1"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(2)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(2)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(2)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(2), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(2)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END

				L = SQRT(POW(Xact , 2) + POW(Yact , 2))
				
				IF (L <> 0)
					ExtractTime = L / gParamValue(5)
					ExtractVelocity = gParamValue(6) / (POW(S1Diameter,2)* PI /4) / (ExtractTime) 
					Portion =  gParamValue(6) / (POW(S1Diameter,2)* PI /4)
				ELSE
					ExtractVelocity = 0
					Portion = 0
				END

				! Скорость в мм/c 
				IF (ExtractVelocity * REDUCER <= XVEL(7))
					!IF (ExtractVelocity > 0)
						IF (ActZ > ActZ + Zactd)
							PTP/rv (7),  Portion, ExtractVelocity * REDUCER
							PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
							PTP/rev (2),  Zactd, gParamValue(5) * REDUCER
						ELSE
							PTP/rev (2),  Zactd, gParamValue(5) * REDUCER
							PTP/rv (7), Portion, ExtractVelocity * REDUCER
							PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
						END
					!ELSE
					!	DISP "S1 extract velocity is equal to 0"
					!END
				ELSE
					DISP "S1 velocity too hight"
				END
				PrevTool = 2
				TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(7).#MOVE

			ELSEIF (gParamValue(7) = 2)					! Шприц 2
			!DISP "Syringe 2"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(3)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(3)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!DISP "CurOffsetX =", CurOffsetX
				!DISP "CurOffsetY =", CurOffsetY
				
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(3)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(3), CurOffsetZ
				
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(3)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
				L = SQRT(POW(Xact , 2) + POW(Yact , 2))
				
				IF (L <> 0)
					ExtractTime = L / gParamValue(5)
					ExtractVelocity = gParamValue(6) / (POW(S2Diameter,2)* PI /4) / (ExtractTime)
					Portion =  gParamValue(6) / (POW(S2Diameter,2)* PI /4)
				ELSE
					ExtractVelocity = 0
					Portion = 0
				END
				
				IF (ExtractVelocity * REDUCER <= XVEL(8))
					!IF (ExtractVelocity > 0)
						IF (ActZ > ActZ + Zactd)
							PTP/rv (8),  Portion, ExtractVelocity * REDUCER
							PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
							PTP/rev (3),  Zactd, gParamValue(5) * REDUCER
						ELSE
							PTP/rev (3),  Zactd, gParamValue(5) * REDUCER
							PTP/rv (8),  Portion, ExtractVelocity * REDUCER
							PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
						END
					!ELSE
					!	DISP "S2 extract velocity is equal to 0"
					!END
				ELSE
					DISP "S2 velocity too hight"
				END
				PrevTool = 3
				TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(8).#MOVE
				!DISP "Motion Syringe OK
				
			ELSEIF (gParamValue(7) = 3)					! Шприц 3
				!DISP "Syringe 3"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(4)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(4)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!DISP "CurOffsetX =", CurOffsetX
				!DISP "CurOffsetY =", CurOffsetY
				
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(4)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				
				!PTP/re(4), CurOffsetZ
				
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(4)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)				
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				L = SQRT(POW(Xact , 2) + POW(Yact , 2))
				
				IF (L <> 0)
					ExtractTime = L / gParamValue(5)
					ExtractVelocity = gParamValue(6) / (POW(S3Diameter,2)* PI /4) / (ExtractTime)
					Portion = gParamValue(6) / (POW(S3Diameter,2)* PI /4)
				ELSE
					ExtractVelocity = 0
					Portion = 0
				END
				
				IF (ExtractVelocity * REDUCER <= XVEL(9))
					!IF (ExtractVelocity > 0)
						IF (ActZ > ActZ + Zactd)
							PTP/rv (9),  Portion, ExtractVelocity * REDUCER
							PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
							PTP/rev (4),  Zactd, gParamValue(5) * REDUCER
						ELSE
							PTP/rev (4),  Zactd, gParamValue(5) * REDUCER
							PTP/rv (9),  Portion, ExtractVelocity * REDUCER
							PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
						END
						!PTP/rv (0, 1), gParamValue(2), gParamValue(3), gParamValue(5)
						!N69 G91
						!N70 G01 X[gParamValue(2)] Y[gParamValue(3)] F[gParamValue(5)]
					!ELSE
					!	DISP "S3 extract velocity is equal to 0"
					!END
					!N71 G04 P0.1
				ELSE
					DISP "S3 velocity too hight"
				END
				PrevTool = 4
				TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(9).#MOVE
				
			ELSEIF (gParamValue(7) = 5)					! Дозатор
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(6)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(6)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(6)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(6), CurOffsetZ
				
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(6)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				L = SQRT(POW(Xact , 2) + POW(Yact , 2))
				
				IF (L <> 0)
					ExtractTime = L / gParamValue(5)
					ExtractVelocity = gParamValue(6) / (POW(PF1Diameter,2)* PI /4) * gParamValue(8)/100 / (ExtractTime)
					ExtractVelocityPF = gParamValue(6) / (POW(PF2Diameter,2)* PI /4)* (1 - gParamValue(8)/100)/ (ExtractTime)
				ELSE
					ExtractVelocity = 0
					ExtractVelocityPF = 0
				END		
				
				
				IF (ActZ > ActZ + Zactd)
					XPF1 = ExtractVelocity * REDUCER
					XPF2 = ExtractVelocityPF * REDUCER
					PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
					PTP/rev (6),  Zactd, gParamValue(5) * REDUCER
				ELSE
					PTP/rev (6),  Zactd, gParamValue(5) * REDUCER
					XPF1 = ExtractVelocity * REDUCER
					XPF2 = ExtractVelocityPF * REDUCER
					PTP/rev (0, 1), Xact, Yact, gParamValue(5) * REDUCER
				END
				!PTP/rv (14),  gParamValue(6) / (POW(PF1Diameter,2)* PI /4) * gParamValue(8)/100, ExtractVelocity
				!PTP/rv (15),  gParamValue(6) / (POW(PF2Diameter,2)* PI /4)* (1 - gParamValue(8)/100), ExtractVelocityPF

				PrevTool = 6
				TILL ^AST(0).#MOVE & ^AST(1).#MOVE
				XPF1 = 0
				XPF2 = 0
			ELSE
				DISP "Wrong Axis"
				STOP	
			END
		
		!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		
		ELSEIF (gParamValue(9) = 2)	! Круговое перемещение G2 по часовой стрелке
		!DISP "Type 2"
		! Проверка на номер дозатора
			IF (gParamValue(7) = 1)						! Шприц 1
			!DISP "Syringe 1"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(2)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(2)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(2)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(2), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(2)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
		!DISP "a = ", a
		!DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
		!DISP "n = ", n
		L = PI * R * n / 180
		
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			Portion = gParamValue(6) / (POW(S1Diameter,2)* PI /4)
			ExtractVelocity = Portion / (ExtractTime)
		ELSE
			Portion = 0
			ExtractVelocity = 0
		END
		
		IF (ExtractVelocity * REDUCER <= XVEL(7))
			!IF (ExtractVelocity > 0)
					IF (ActZ > ActZ + Zactd)
						PTP/rv (7),  Portion, ExtractVelocity * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (2),  Zactd, gParamValue(5) * REDUCER
					ELSE
						PTP/rev (2),  Zactd, gParamValue(5) * REDUCER
						PTP/rv (7),  Portion, ExtractVelocity * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
					END
				!OffsetZ = FPOS(2)
			!ELSE
			!	DISP "S1 extract velocity is equal to 0"
			!END
		ELSE
			DISP "S1 velocity too hight"
		END
		PrevTool = 2
		TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(7).#MOVE
			
		ELSEIF (gParamValue(7) = 2)					! Шприц 2
					!DISP "Syringe 1"
			IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(3)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(3)
			ELSE
					CurOffsetX = 0
					CurOffsetY = 0
			END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(3)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(3), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(3)
			IF (WORKMODE = 90)
				IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
				ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
				ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
				END
			ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
			END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
		!DISP "a = ", a
		!DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
		!DISP "n = ", n
		L = PI * R * n / 180 
		
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(S2Diameter,2)* PI /4)/(ExtractTime)
			Portion = gParamValue(6) / (POW(S2Diameter,2)* PI /4)
		ELSE
			Portion = 0
			ExtractVelocity = 0
		END
		

		IF (ExtractVelocity * REDUCER <= XVEL(8))
			!IF (ExtractVelocity > 0)
					IF (ActZ > ActZ + Zactd)
						PTP/rv (8),  Portion, ExtractVelocity * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (3),  Zactd, gParamValue(5) * REDUCER
					ELSE
						PTP/rev (3),  Zactd, gParamValue(5) * REDUCER
						PTP/rv (8),  Portion, ExtractVelocity * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
					END
			!ELSE
			!	DISP "S2 extract velocity is equal to 0"
				!OffsetZ = FPOS(3)
			!END
		ELSE
			DISP "S2 velocity too hight"
		END
		PrevTool = 3
		TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(8).#MOVE

		ELSEIF (gParamValue(7) = 3)					! Шприц 3
			
					!	DISP "Syringe 1"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(4)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(4)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(4)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(4), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(4)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
		!DISP "a = ", a
		!DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
		!DISP "n = ", n
		L = PI * R * n / 180 
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(S3Diameter,2)* PI /4)/(ExtractTime)
			Portion = gParamValue(6) / (POW(S3Diameter,2)* PI /4)
		ELSE
			Portion = 0
			ExtractVelocity = 0
		END
		
		IF (ExtractVelocity * REDUCER <= XVEL(9))
			!IF (ExtractVelocity > 0)
					IF (ActZ > ActZ + Zactd)
						PTP/rv (9),  Portion, ExtractVelocity * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (4),  Zactd, gParamValue(5) * REDUCER
					ELSE
						PTP/rev (4),  Zactd, gParamValue(5) * REDUCER
						PTP/rv (9),  Portion, ExtractVelocity * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
					END
			!ELSE
				!	DISP "S3 extract velocity is equal to 0"
				!OffsetZ = FPOS(4)
			!END
		ELSE
			DISP "S3 velocity too hight"
		END
		PrevTool = 4
		TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(9).#MOVE

		ELSEIF (gParamValue(7) = 5)					! Дозатор
					!DISP "Syringe 1"
			IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(6)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(6)
			ELSE
					CurOffsetX = 0
					CurOffsetY = 0
			END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(6)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(6), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(6)
			IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
			ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
			END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
	!	DISP "a = ", a
	!	DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
	!	DISP "n = ", n
		L = PI * R * n / 180 
		
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(PF1Diameter,2)* PI /4) * gParamValue(8)/100 /(ExtractTime)
			ExtractVelocityPF = gParamValue(6) / (POW(PF2Diameter,2)* PI /4)* (1 - gParamValue(8)/100)/ExtractTime
		ELSE
			ExtractVelocity = 0
			ExtractVelocityPF = 0
		END

				IF (ActZ > ActZ + Zactd)
						XPF1 = ExtractVelocity * REDUCER
						XPF2 = ExtractVelocityPF * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (6),  Zactd, gParamValue(5) * REDUCER
				ELSE
						PTP/rev (6),  Zactd, gParamValue(5) * REDUCER
						XPF1 = ExtractVelocity * REDUCER
						XPF2 = ExtractVelocityPF * REDUCER
						MSEG/v (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
				END

				!PTP/rv (14),  gParamValue(6) / (POW(PF1Diameter,2)* PI /4) * gParamValue(8)/100, ExtractVelocity
				!PTP/rv (15),  gParamValue(6) / (POW(PF2Diameter,2)* PI /4)* (1 - gParamValue(8)/100), ExtractVelocityPF

				PrevTool = 6
				TILL ^AST(0).#MOVE & ^AST(1).#MOVE
				XPF1 = 0
				XPF2 = 0
			ELSE
				DISP "Wrong Axis"
				STOP
			END
		
		!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		ELSEIF (gParamValue(9) = 3) ! Круговое перемещение G3 против часовой стрелки
		!DISP "Type 3"
		! Проверка на номер дозатора
			IF (gParamValue(7) = 1)						! Шприц 1
						!DISP "Syringe 1"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(2)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(2)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(2)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(2), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(2)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
		!DISP "a = ", a
		!DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
		!DISP "n = ", n
		L = PI * R * n / 180 
		
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(S1Diameter,2)* PI /4)/(ExtractTime)
			Portion = gParamValue(6) / (POW(S1Diameter,2)* PI /4)
		ELSE
			Portion = 0
			ExtractVelocity = 0
		END		
		
		IF (ExtractVelocity * REDUCER <= XVEL(7))
			!IF (ExtractVelocity > 0)
					IF (ActZ > ActZ + Zactd)
						PTP/rv (7),  Portion, ExtractVelocity * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (2),  Zactd, gParamValue(5) * REDUCER
					ELSE
						PTP/rev (2),  Zactd, gParamValue(5) * REDUCER
						PTP/rv (7), Portion, ExtractVelocity * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
					END
			!ELSE
			!	DISP "S1 extract velocity is equal to 0"
				!OffsetZ = FPOS(2)
			!END
		ELSE
			DISP "S1 velocity too hight"
		END
		PrevTool = 2
		TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(7).#MOVE
			
		ELSEIF (gParamValue(7) = 2)					! Шприц 2
						!DISP "Syringe 1"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(3)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(3)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(3)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(3), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(3)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
		!DISP "a = ", a
		!DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
	!	DISP "n = ", n
		L = PI * R * n / 180 

		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(S2Diameter,2)* PI /4)/(ExtractTime)
			Portion = gParamValue(6) / (POW(S2Diameter,2)* PI /4)
		ELSE
			Portion = 0
			ExtractVelocity = 0
		END	
		
		IF (ExtractVelocity * REDUCER <= XVEL(8))
			!IF (ExtractVelocity > 0)
					IF (ActZ > ActZ + Zactd)
						PTP/rv (8),  Portion, ExtractVelocity * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (3),  Zactd, gParamValue(5) * REDUCER
					ELSE
						PTP/rev (3),  Zactd, gParamValue(5) * REDUCER
						PTP/rv (8),  Portion, ExtractVelocity * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
					END
			!ELSE
			!		DISP "S2 extract velocity is equal to 0"
			!END
				!OffsetZ = FPOS(3)
		ELSE
			DISP "S2 velocity too hight"
		END
		PrevTool = 3
		TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(8).#MOVE
				
		ELSEIF (gParamValue(7) = 3)					! Шприц 3
					!	DISP "Syringe 1"
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(4)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(4)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(4)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(4), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(4)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
	!	DISP "a = ", a
	!	DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
		!DISP "n = ", n
		L = PI * R * n / 180 
		
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(S3Diameter,2)* PI /4)/(ExtractTime)
			Portion = gParamValue(6) / (POW(S3Diameter,2)* PI /4)
		ELSE
			Portion = 0
			ExtractVelocity = 0
		END	
		
		IF (ExtractVelocity * REDUCER <= XVEL(9))
			!IF (ExtractVelocity > 0)
					IF (ActZ > ActZ + Zactd)
						PTP/rv (9), Portion, ExtractVelocity * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (4),  Zactd, gParamValue(5) * REDUCER
					ELSE
						PTP/rev (4),  Zactd, gParamValue(5) * REDUCER
						PTP/rv (9), Portion, ExtractVelocity * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
					END
			!ELSE
			!		DISP "S2 extract velocity is equal to 0"
			!END
				!OffsetZ = FPOS(4)
		ELSE
			DISP "S3 velocity too hight"
		END
		PrevTool = 4
		TILL ^AST(0).#MOVE & ^AST(1).#MOVE & ^AST(9).#MOVE
				
		ELSEIF (gParamValue(7) = 5)					! Дозатор
				IF (PrevTool <> 0)
					CurOffsetX = -OffsetX(PrevTool) + OffsetX(6)
					CurOffsetY = -OffsetY(PrevTool) + OffsetY(6)
				ELSE
					CurOffsetX = 0
					CurOffsetY = 0
				END
				!IF (OffsetZ <> 0)
				!	CurOffsetZ = OffsetZ - FPOS(6)
				!ELSEIF (OffsetZ = ZGLOBAL)
				!	CurOffsetZ = 0
				!ELSE
				!	CurOffsetZ = 0
				!END
				PTP/re(0, 1), CurOffsetX, CurOffsetY
				!PTP/re(6), CurOffsetZ
				ActX = FPOS(0)
				ActY = FPOS(1)
				ActZ = FPOS(6)
				IF (WORKMODE = 90)
					IF (StartTool = gParamValue(7) + 1)
						Xact = -ActX + XGLOBAL + gParamValue(2)
						Yact = -ActY + YGLOBAL + gParamValue(3)	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSEIF (PrevTool = gParamValue(7) + 1)
						Xact = 0
						Yact = 0	
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					ELSE 
						Xact = -ActX + XGLOBAL + gParamValue(2) + CurOffsetX
						Yact = -ActY + YGLOBAL + gParamValue(3) + CurOffsetY
						Zactd = -ActZ + ZGLOBAL + gParamValue(4)
					END
				ELSE
					Xact = gParamValue(2)
					Yact = gParamValue(3)
					Zactd = gParamValue(4)
				END
				
		R = SQRT(POW(gParamValue(10),2) + POW(gParamValue(11),2))
		!DISP "R = ", R
		a = (ActX * Xact) 
		b = (ActY * Yact)
	!	DISP "a = ", a
	!	DISP "b = ", b
		IF ((ActX = 0) & (ActY = 0) & (Xact = 0) & (Yact = 0))
			n = 0
		ELSEIF ((ActX <> 0) & (ActY <> 0) & (Xact = 0) & (Yact = 0))
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2))))
		ELSEIF ((ActX = 0) & (ActY = 0) & (Xact <> 0) & (Yact <> 0))
			n = ACOS((a + b) / (SQRT(POW(Xact ,2) + POW(Yact ,2))))
		ELSE
			n = ACOS((a + b) / (SQRT(POW(ActX ,2) + POW(ActY ,2)) * SQRT(POW(Xact ,2) + POW(Yact ,2))))
		END
	!	DISP "n = ", n
		L = PI * R * n / 180 
		
		IF (L <> 0)
			ExtractTime = L / gParamValue(5)
			ExtractVelocity = gParamValue(6) / (POW(PF1Diameter,2)* PI /4) * gParamValue(8)/100 /(ExtractTime)
			ExtractVelocityPF = gParamValue(6) / (POW(PF2Diameter,2)* PI /4)* (1 - gParamValue(8)/100)/(ExtractTime)	
		ELSE
			ExtractVelocity = 0
			ExtractVelocityPF = 0
		END	

				IF (ActZ > ActZ + Zactd)
						XPF1 = ExtractVelocity * REDUCER
						XPF2 = ExtractVelocityPF * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
						PTP/rev (6),  Zactd, gParamValue(5) * REDUCER
				ELSE
						PTP/rev (6),  Zactd, gParamValue(5) * REDUCER
						XPF1 = ExtractVelocity * REDUCER
						XPF2 = ExtractVelocityPF * REDUCER
						MSEG (0, 1), FPOS(0), FPOS(1)
						ARC1/v (0, 1), gParamValue(10), gParamValue(11), Xact, Yact, +, gParamValue(5) * REDUCER
						ENDS (0, 1)
				END			

				!PTP/rv (14),  gParamValue(6) / (POW(PF1Diameter,2)* PI /4) * gParamValue(8)/100, ExtractVelocity
				!PTP/rv (15),  gParamValue(6) / (POW(PF2Diameter,2)* PI /4)* (1 - gParamValue(7)/100), ExtractVelocityPF
				!OffsetZ = FPOS(6)
				PrevTool = 6
				TILL ^AST(0).#MOVE & ^AST(1).#MOVE
				XPF1 = 0
				XPF2 = 0
			ELSE
				DISP "Wrong Axis"
				STOP
			END
		ELSE
			DISP "Wrong Motion"
			STOP
		END
	ELSE
		DISP "Not enough parameters"
		STOP
	END
RET

! Команда G89 - всасывание
G89:
PI = 3.14159
	IF ((gParamAddr(2)=80) | (gParamAddr(3)=80) | (gParamAddr(4)=80))
		IF 		(gParamValue(3)=1)														! Шприц 1
			PTP/re (7), -gParamValue(2)/(POW(S1Diameter,2)* PI /4)							! Всасывание
			!STOP
		ELSEIF 	(gParamValue(3)=2)														! Шприц 2
			PTP/re (8), -gParamValue(2)/(POW(S2Diameter,2)* PI /4)							! Всасывание
			!STOP
		ELSEIF 	(gParamValue(3)=3)														! Шприц 3
			PTP/re (9), -gParamValue(2)/(POW(S3Diameter,2)* PI /4)							! Всасывание
			!STOP
		ELSEIF 	(gParamValue(3)=5)														! Дозатор
			PTP/re (14), -gParamValue(2)*gParamValue(4)/100/(POW(PF1Diameter,2)* PI /4)		! Всасывание
			PTP/re (15), -gParamValue(2)*(1-gParamValue(4)/100)/(POW(PF2Diameter,2)* PI /4) ! Всасывание
			!STOP
		ELSE
			DISP "Wrong Axis"
			STOP
		END
	ELSE
		DISP "Wrong Parameters"
		STOP
	END
RET

