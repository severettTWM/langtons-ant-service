using Microsoft.AspNetCore.Mvc;
using LangtonsAntApi.Features;
using LangtonsAntApi.Models;

namespace LangtonsAntApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CellsController : ControllerBase
{
    private readonly ILogger<CellsController> _logger;
    private readonly IUpdateCells _updateCells;

    public CellsController(ILogger<CellsController> logger, IUpdateCells updateCells)
    {
        _logger = logger;
        _updateCells = updateCells;
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
        
        return _updateCells.Execute(cells, numberOfMoves);
    }
}
