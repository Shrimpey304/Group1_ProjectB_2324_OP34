namespace Cinema;

public class Seating{

    private int currentID = 1;
    public int Rows;
    public int Columns;

    ///<summary>
    ///<para>multidimensional array: represents the cinema room</para>
    ///<para>List: represents a seat</para>
    ///<para>Dictionary: data per seat, can be multiple in the list</para>
    ///</summary>
    public List<Dictionary<string, string>>[,] SeatingArrangement;

    public Seating(int rows, int columns){

        Rows = rows;
        Columns = columns;
        SeatingArrangement = new List<Dictionary<string, string>>[rows , columns];
        InitializeSeatingArrangement();
    }

    public List<Dictionary<string, string>> getSeatList(bool wantSeatData){

        try{
            if(!wantSeatData){

                foreach(List<Dictionary<string, string>> seat in SeatingArrangement){
                    return seat;
                }

            }else{

                foreach(List<Dictionary<string, string>> seat in SeatingArrangement){
                    getSeatData(seat);
                }
            }
        }catch(NullReferenceException){

            return null!;
        }

        return null!;
    }

    public Dictionary<string, string> getSeatData(List<Dictionary<string, string>> seat){

        if(seat != null){

            foreach(Dictionary<string, string> Datarow in seat){
                return Datarow;
            }
        }

        return null!;
    }

    private void InitializeSeatingArrangement(){

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                SeatingArrangement[i, j] = new List<Dictionary<string, string>>();
                AssignIDToSeating(i, j);
            }
        }
    }

    private void AssignIDToSeating(int row, int column){

        string uniqueID = currentID.ToString();
        currentID++;

        Dictionary<string, string> seatingInfo = new Dictionary<string, string>
        {
            { "ID", uniqueID },
        };

        SeatingArrangement[row, column].Add(seatingInfo);
    }

}