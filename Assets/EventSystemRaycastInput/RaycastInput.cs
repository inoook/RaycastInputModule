using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastInput : BaseInput
{
    // NOTE: RenderTextureで描画する Canvas は Screen Space - Camera に設定
    // textureCoord を使用するには collider は MeshCollider でなければならない. MeshCollider で Convexも不可。

    public override Vector2 mousePosition {
        get { return GetRaycastPosition(Input.mousePosition); }
    }

    public override Touch GetTouch(int index) {
        Touch touch = Input.GetTouch(index);
        touch.position = GetRaycastPosition(touch.position);
        return touch;
    }

    [SerializeField] Camera viewCamera = null;

    [SerializeField] float maxDistance = 10;
    [SerializeField] LayerMask layerMask = 0;
    [SerializeField] bool debugDrawHitNormal = false;

    Vector2 raycastPos;
    InteractiveObject currentInteractiveObject;

    private Vector2 GetRaycastPosition(Vector2 inputPos) {

        Ray ray = viewCamera.ScreenPointToRay(inputPos);
        RaycastHit hitInfo;
        //RenderTexture renderTexture;
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask)) {
            if (debugDrawHitNormal) {
                Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red);
            }
            GameObject gObj = hitInfo.collider.gameObject;

            if (currentInteractiveObject != null) {
                currentInteractiveObject.Select(false);
            }
            InteractiveObject interactiveObject = gObj.GetComponent<InteractiveObject>();
            if (interactiveObject != null) {
                currentInteractiveObject = interactiveObject;
                currentInteractiveObject.Select(true);
            }

            Texture texture = gObj.GetComponent<Renderer>().material.mainTexture;
            Vector2 rtTextureSize = new Vector2(texture.width, texture.height);

            //Vector2 rtTextureSize = new Vector2(rtCamera.targetTexture.width, rtCamera.targetTexture.height);
            Vector2 clickPos = Vector2.Scale(hitInfo.textureCoord, rtTextureSize);
            raycastPos = clickPos;
        }
        else {
            if (currentInteractiveObject != null) {
                currentInteractiveObject.Select(false);
                currentInteractiveObject = null;
            }
            raycastPos = new Vector2(-100, -100);// Meshの範囲外
        }
        return raycastPos;
    }
}
