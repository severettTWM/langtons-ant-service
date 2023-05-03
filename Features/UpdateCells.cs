using  LangtonsAntApi.Models;
namespace LangtonsAntApi.Features;

public class UpdateCells : IUpdateCells
{
    public List<List<int>> Execute(List<List<int>> cells, int numberOfMoves)
    {
        int antDirection = Directions.ANTRIGHT;
        int xDimension = cells.Count();
        int yDimension = cells[0].Count();
        Coordinates antCoordinates = new Coordinates() {X = xDimension/2, Y = yDimension/2 };

        for (int i=0; i<numberOfMoves; ++i) {
            for (int j=0; j<xDimension; j++) {
                for (int k=0; k<yDimension; ++k) {
                    if (j == antCoordinates.X && k == antCoordinates.Y) {
                        if (cells[j][k] == 1) {
                            int leftTurn = ((antDirection - 1) + (Directions.ANTLEFT + 1)) % (Directions.ANTLEFT + 1);
                            antDirection = leftTurn;
                        } else {
                            int rightTurn = ((antDirection + 1) + (Directions.ANTLEFT + 1)) % (Directions.ANTLEFT + 1);
                            antDirection = rightTurn;
                        }
                        cells[j][k] = (cells[j][k] + 1) % 2;
                        continue;
                    }
                }
            }
            antCoordinates = MoveForward(antDirection, antCoordinates);
        }

        return cells;
    }

    private Coordinates MoveForward(int currentDirection, Coordinates coordinates) {
        var newCoordinates = new Coordinates() {X = coordinates.X, Y = coordinates.Y };
        switch (currentDirection) {
          case Directions.ANTUP:
            newCoordinates.X = newCoordinates.X - 1;
            break;
          case Directions.ANTRIGHT:
            newCoordinates.Y = newCoordinates.Y + 1;
            break;
          case Directions.ANTDOWN:
            newCoordinates.X = newCoordinates.X + 1;
            break;
          case Directions.ANTLEFT:
            newCoordinates.Y = newCoordinates.Y - 1;
            break;
        }
        return newCoordinates;
    }
}