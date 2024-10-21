using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellModels : IGameService, IModelProvider<CellModel>
{
    public CellModels()
    {
        ServiceLocator.AddService<CellModels>(this);
    }

    public void AddItem(CellModel item)
    {
        Container<CellModel>.AddItem(item);
    }

    public void RemoveItem(CellModel item)
    {
        Container<CellModel>.RemoveItem(item);
    }

    public CellModel Get(Vector2Int pos)
    {
        CellModel model = Container<CellModel>.FindItem((x) => x.Pos == pos);

        if (model == null)
            model = new CellModel(pos);
        Container<CellModel>.AddItem(model);

        return model;
    }
}
