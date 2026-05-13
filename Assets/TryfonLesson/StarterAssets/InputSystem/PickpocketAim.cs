using StarterAssets;
using UnityEngine;
using Unity.Cinemachine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    public ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    public LayerMask mouseColliderMask;
    public Transform aimObject;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseworldPosition = Vector3.zero;
        Vector2 screenCenterPoint =  new Vector2(Screen.width/2 , Screen.height/2); //find middle
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit rayHit, 999f, mouseColliderMask))
        {
            aimObject.transform.position = rayHit.point;
            mouseworldPosition = rayHit.point;
        }


        if(starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetRotationOnMove(false);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime * 10));
            Vector3 worldAimTarget = mouseworldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget- transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection , Time.deltaTime*20);


        }else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotationOnMove(true);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10));
        }

    }
}
