namespace LangtonsAntApi.Features;
public interface IUpdateCells {
    List<List<int>> Execute(List<List<int>> cells, int numberOfMoves);
}