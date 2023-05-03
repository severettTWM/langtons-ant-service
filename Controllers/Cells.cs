using Microsoft.AspNetCore.Mvc;

namespace LangtonsAntApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CellsController : ControllerBase
{
    private readonly ILogger<CellsController> _logger;
    private const int ANTUP = 0;
    private const int ANTRIGHT = 1;
    private const int ANTDOWN = 2;
    private const int ANTLEFT = 3;

    public CellsController(ILogger<CellsController> logger)
    {
        _logger = logger;
    }
    public class Coordinates {
        public int X { get; set; }
        public int Y { get; set; }
    }

    [HttpGet(Name = "GetCells")]
    public IEnumerable<IEnumerable<int>> Get(int xDimension, int yDimension, int numberOfMoves)
    {

        List<List<int>> cells = new List<List<int>>();
        for (int i=0; i<xDimension; ++i) {
            cells.Add(new List<int>());
            for (int j=0; j<yDimension; ++j) {
                cells[i].Add(0);
            }
        }
        
        return UpdateCells(cells, numberOfMoves);
    }

    public List<List<int>> UpdateCells(List<List<int>> cells, int numberOfMoves)
    {
        int antDirection = ANTRIGHT;
        int xDimension = cells.Count();
        int yDimension = cells[0].Count();
        Coordinates antCoordinates = new Coordinates() {X = xDimension/2, Y = yDimension/2 };

        for (int i=0; i<numberOfMoves; ++i) {
            for (int j=0; j<xDimension; j++) {
                for (int k=0; k<yDimension; ++k) {
                    if (j == antCoordinates.X && k == antCoordinates.Y) {
                        if (cells[j][k] == 1) {
                            int leftTurn = ((antDirection - 1) + (ANTLEFT + 1)) % (ANTLEFT + 1);
                            antDirection = leftTurn;
                        } else {
                            int rightTurn = ((antDirection + 1) + (ANTLEFT + 1)) % (ANTLEFT + 1);
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

    public Coordinates MoveForward(int currentDirection, Coordinates coordinates) {
        var newCoordinates = new Coordinates();
        switch (currentDirection) {
          case ANTUP:
            newCoordinates.X = newCoordinates.X - 1;
            break;
          case ANTRIGHT:
            newCoordinates.Y = newCoordinates.Y + 1;
            break;
          case ANTDOWN:
            newCoordinates.X = newCoordinates.X + 1;
            break;
          case ANTLEFT:
            newCoordinates.Y = newCoordinates.Y - 1;
            break;
        }
        return newCoordinates;
    }

    
}
