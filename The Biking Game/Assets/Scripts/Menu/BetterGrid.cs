using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetterGrid : LayoutGroup
{
    public bool HideHidden;
    public List<Transform> VisibleChildren;
    public enum FitType{
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }
    public FitType fitType;
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public bool fitX;
    public bool fitY;
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        if(HideHidden)
        {
            VisibleChildren = new List<Transform>();
            foreach (Transform child in transform)
            {
                if(child.gameObject.activeSelf){
                    VisibleChildren.Add(child);
                }
            }
        }
        if(fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform){
        fitX = true;
        fitY = true;
        float sqrRt = Mathf.Sqrt(HideHidden ? VisibleChildren.Count : transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        columns = Mathf.CeilToInt(sqrRt);
        }
        if(fitType == FitType.Width || fitType == FitType.FixedColumns){
            rows = Mathf.CeilToInt((HideHidden ? VisibleChildren.Count : transform.childCount) / (float) columns);
        }
        if(fitType == FitType.Height || fitType == FitType.FixedRows){
            columns = Mathf.CeilToInt((HideHidden ? VisibleChildren.Count : transform.childCount) / (float) rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth/ (float)columns) - ((spacing.x / (float)columns)*2) - (padding.left/ (float) columns) - (padding.right / (float)columns);
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows)*2) - (padding.top/ (float) rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth: cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for(int i = 0; i < ((HideHidden ? VisibleChildren.Count : rectChildren.Count)); i++){
            rowCount = i / columns;
            columnCount = i % columns;

            var item = (RectTransform)(HideHidden ? VisibleChildren[i] : rectChildren[i]);

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount)+ padding.top;
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    public override void CalculateLayoutInputVertical()
    {
        return;
    }
    public override void SetLayoutHorizontal()
    {
        return;
    }
    public override void SetLayoutVertical()
    {
        return;
    }
}
