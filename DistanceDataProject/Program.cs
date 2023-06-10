using System;
using System.Xml;

public class Passengers
{
    public string name = default!;
    public int seated;
    public int seatNumber; // seat number = 0, means the passenger seat not known yet.
    public int startingIndex;
    public int type;
    public double distance;

    public Passengers()
    {
    }

    public Passengers(string name, int seated, int startingIndex)
    {
        this.name = name;
        this.seated = seated;
        this.startingIndex = startingIndex;
    }
}

public class Program
{
    public static int matrixRowsLength = 40;
    public static int matrixColumnsLength = 40;
    public static int busRowsLength = 4;

    public static void Main(string[] args)
    {

        string[] passengersName =
        {
            "Ahmet", "Adem", "Ali", "Akif", "Asli", "Arda", "Aylin", "Arif", "Beyhan", "Buray", "Buket", "Büşra",
            "Beyza", "Başak", "Ceyda", "Ceylin", "Ceren", "Didem", "Derya",
            "Demet", "Dursun", "Elif", "Eren", "Eda", "Esra", "Ela", "Fatih", "Firat", "Fadime", "Gokay", "Hatice",
            "Halime", "Hafize", "Habibe", "Ikra", "Ilayda", "Jale",
            "Kemal", "Koray", "Leyla"
        };
        
        Passengers[] passengersArr = PassengerArrCreator(passengersName);
        double[,] distanceMatrix = DistanceMatrixCreator(matrixRowsLength, matrixColumnsLength);

        WriteMatrix(distanceMatrix);
        CreateSeatsForPassengers(passengersArr, distanceMatrix);
        Passengers[,] passengerSeatMatrix = CreateSeatMatrix(passengersArr);
        WritePassengerMatrix(passengerSeatMatrix);
        double total = TotalDistance(passengersArr);
        Console.WriteLine("Total Distance = " + total);
        Console.WriteLine("------------------------------------------------------------");
        WritePassengersDistance(passengersArr);

        Console.ReadLine();

    }

    public static void CreateSeatsForPassengers(Passengers[] _passengersArray, double[,] _distanceMatrix)
    {
        Random r = new Random();
        int luckyNumber = r.Next(_distanceMatrix.GetLength(0));
        _passengersArray[luckyNumber].seatNumber = 1;
        _passengersArray[luckyNumber].seated = 1;
        _passengersArray[luckyNumber].type = +2;
        _passengersArray[luckyNumber].distance = 0;
        Passengers seatedPassenger;

        for (int i = 1; i < _distanceMatrix.GetLength(0); i++) // i means the seat numbers. Start point
        {
            
            seatedPassenger = FindLowDistance(_passengersArray, _distanceMatrix, i+1);
            seatedPassenger.seatNumber = i + 1;
            seatedPassenger.seated = 1;
            seatedPassenger.type = TypeFinder(i + 1);


            
        }
    }

    public static Passengers FindLowDistance(Passengers[] _passengersArray, double[,] _distanceMatrix, int _seatNumber)
    {
        double minimumDistance = 9999.99;
        int minDistanceIndex = 0;
        for (int i = 0; i < _distanceMatrix.GetLength(0); i++)
        {
            if (_passengersArray[i].seated == 0) // just trying the correct passenger for minimum distance.
            {
                _passengersArray[i].type = TypeFinder(_seatNumber);
                _passengersArray[i].seatNumber = _seatNumber;
                if (minimumDistance > FindDistance(_passengersArray, _distanceMatrix, _passengersArray[i]))
                {
                    minimumDistance = FindDistance(_passengersArray, _distanceMatrix, _passengersArray[i]);
                    minDistanceIndex = i;
                }
                _passengersArray[i].type = -3;                // type = -3 means the seat number not known yet
                _passengersArray[i].seatNumber = 0;    // And We don't know yet because of the, maybe this passenger doesn't have the least distance.
            }

        }
        _passengersArray[minDistanceIndex].distance = minimumDistance;
        return _passengersArray[minDistanceIndex]; // After the loop, we know the passenger is %100 percent correct passenger.
    }

    public static double FindDistance(Passengers[] _passengersArray, double[,] _distanceMatrix, Passengers _passenger)
    {
        double minimumDistance = 9999.99;
        double distanceLeftUp = 9999.99;
        double distanceUp = 9999.99;
        double distanceRightUp = 9999.99;
        double distanceLeft = _distanceMatrix[GetPassengerFromSeatNumber(_passengersArray, _passenger.seatNumber - 1).startingIndex, _passenger.startingIndex];

        int type = _passenger.type;

        if (type == 0 || type == +1)
        {
            distanceLeftUp = _distanceMatrix[GetPassengerFromSeatNumber(_passengersArray, _passenger.seatNumber - busRowsLength - 1).startingIndex, _passenger.startingIndex];
        }

        if (type != -2)
        {
            distanceUp = _distanceMatrix[GetPassengerFromSeatNumber(_passengersArray, _passenger.seatNumber - busRowsLength).startingIndex, _passenger.startingIndex];
        }

        if (type == -1 || type == +1)
        {
            distanceRightUp = _distanceMatrix[GetPassengerFromSeatNumber(_passengersArray, _passenger.seatNumber - busRowsLength + 1).startingIndex, _passenger.startingIndex];
        }

        switch (type)
        {
            case -2:
                minimumDistance = distanceLeft;
                break;
            case -1:
                minimumDistance = distanceUp + distanceRightUp;
                break;
            case 0:
                minimumDistance = distanceLeft + distanceUp + distanceLeftUp;
                break;
            case 1:
                minimumDistance = distanceLeft + distanceUp + distanceLeftUp + distanceRightUp;
                break;
            case 2:
                minimumDistance = 0;
                break;
        }

        return minimumDistance;
    }

    public static int TypeFinder(int _seatNumber)
    {
        //type = -3   // it means seat number not known yet.
        //type = -2;  // it means this is the first row.     (First row seats.)
        //type = -1;  // it means this is the first column.  (First column seats.)
        //type = 0;   // it means this the last column.      (Last column seats.)
        //type = +1;  // it means this is the other columns. (Other column seats)
        //type = +2   // it means this is the first seat.
        int type;

        if (_seatNumber <= busRowsLength)
            type = -2;
        else if (_seatNumber % busRowsLength == 1)
            type = -1;
        else if (_seatNumber % busRowsLength == 0 )
            type = 0;
        else
            type = +1;

        return type;
    }

    public static Passengers GetPassengerFromSeatNumber(Passengers[] _passengersArray, int _seatNumber)
    {
        int currentIndex = 0;
        while (currentIndex < _passengersArray.Length)
        {
            if (_passengersArray[currentIndex].seatNumber == _seatNumber)
            {
                return _passengersArray[currentIndex];
            }
            currentIndex++;
        }
        return _passengersArray[currentIndex-1];
    }

    public static void WriteMatrix(double[,] _matrix)
    {
        var rowsLenght = _matrix.GetLength(0);
        var columnsLenght = _matrix.GetLength(1);


        Console.Write("        ");

        for (var i = 0; i < columnsLenght; i++)
            if (i < 10)
                Console.Write("|" + (i) + "|   ");
            else
                Console.Write("|" + (i) + "|  ");
        Console.WriteLine("");


        for (var i = 0; i < rowsLenght; i++)
        {
            if (i < 10)
                Console.Write("\n" + (i) + " .|    ");
            else
                Console.Write("\n" + (i) + ".|    ");

            for (var j = 0; j < columnsLenght; j++) Console.Write("" + string.Format("{0:0.00}", _matrix[i, j]) + "  ");
        }

        Console.Write("\n\n");
    }

    public static double[,] DistanceMatrixCreator(int matrixRows, int matrixColumns)
    {
        var matrixDistance = new double[matrixRows, matrixColumns];


        for (var i = 0; i < matrixRows; i++)
        for (var j = 0; j < matrixColumns; j++)
            if (i != j)
                matrixDistance[i, j] = RandomReturner();
            else
                matrixDistance[i, j] = 0.0;
        for (var i = 0; i < matrixRows; i++)
        for (var j = 0; j < matrixColumns; j++)
            matrixDistance[j, i] = matrixDistance[i, j];


        return matrixDistance;
    }

    public static Passengers[,] CreateSeatMatrix(Passengers[] _passengersArray) 
    {
        int busColumnsLength = 10;
        Passengers[,] passengersMatrix = new Passengers[busRowsLength, busColumnsLength];
        int _seatNumber = 1;
        for (int i = 0; i < busColumnsLength; i++)
        {
            for (int j = 0; j < busRowsLength; j++)
            {
                passengersMatrix[j, i] = GetPassengerFromSeatNumber(_passengersArray, _seatNumber++ );
            }
        }


        return passengersMatrix;
    }

    public static double RandomReturner()
    {
        var r = new Random();
        return r.NextDouble() * 10d;
    }

    public static Passengers[] PassengerArrCreator(string[] _passengersName)
    {
        var _startingIndex = 0;
        var passengerArr = new Passengers[_passengersName.Length];

        for (var i = 0; i < _passengersName.Length; i++)
            passengerArr[i] = new Passengers(_passengersName[i], 0, _startingIndex++);
        return passengerArr;
    }

    public static void WritePassengerMatrix(Passengers[,] _passengerMatrix)
    {
        int toStringLength;
        int maxStringLengthFor1Passenger = 50;
        for (int i = 0; i < _passengerMatrix.GetLength(1); i++)
        {
            for (int j = 0; j < _passengerMatrix.GetLength(0); j++)
            {
                toStringLength = ToString(_passengerMatrix[j, i]).Length;
                Console.Write(ToString(_passengerMatrix[j, i])   );
                for (int k = 0; k < maxStringLengthFor1Passenger - toStringLength; k++) {
                    Console.Write(" ");
                }
                if (j != busRowsLength - 1) {
                    Console.Write("|");
                }
                if (j % busRowsLength == 0 && j != 0) 
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            for (int l = 0; l < busRowsLength * maxStringLengthFor1Passenger; l++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
        }

    }

    public static double TotalDistance(Passengers[] pList)
    {
        double totalDistance = 0;
        for (int i = 0; i < pList.Length; i++)
        {
            totalDistance += pList[i].distance;
        }

        return totalDistance;


    }

    public static string ToString(Passengers p)
    {
        return (p.seatNumber + ", " + p.name + ", " + String.Format("{0:0.00}", p.distance) + ", Starting Index = " + p.startingIndex);
    }

    public static void WritePassengersDistance(Passengers[] passengers)
    {
        for(int i = 0; i < passengers.Length; i++ )
            Console.WriteLine("Koltuk No = " + passengers[i].seatNumber + ", Distance = " + passengers[i].distance);
    }
}