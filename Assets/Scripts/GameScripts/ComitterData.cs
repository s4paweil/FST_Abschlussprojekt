using System.Collections;
using System.Collections.Generic;
using Boa;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ComitterData : MonoBehaviour, IMixedRealityTouchHandler
{
    private Committer _committer;
    private ToolTip  _tooltip;
    private Color _c;

    private static readonly int Color1 = Shader.PropertyToID("_Color");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCommiter(Committer committer)
    {
        this._committer = committer;
    }

    public Committer getCommitter()
    {
        return this._committer;
    }

    public void setToolTop(ToolTip tooltip)
    {
        _tooltip = tooltip;
    }
    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        _c = this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.GetColor(Color1);
        this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor(Color1, Color.magenta);
        _tooltip.gameObject.SetActive(true);
        _tooltip.ToolTipText = this._committer.Name + "\n" + this._committer.NumberCommitts;
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor(Color1, _c);
        _tooltip.gameObject.SetActive(false);
        _tooltip.ToolTipText = "";
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        
    }
}
