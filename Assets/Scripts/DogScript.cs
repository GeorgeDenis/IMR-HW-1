using UnityEngine;

public class DogScript : MonoBehaviour
{
    private Animator mAnimator;
    public Transform otherCharacter;
    public float attackDistance = 0.25f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    private bool hasDied = false;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.SetTrigger("Idle");
    }

    void Update()
    {
        float distanceToCharacter = Vector3.Distance(transform.position, otherCharacter.position);

        if (!hasDied)
        {
            if (distanceToCharacter < attackDistance && !isAttacking)
            {
                isAttacking = true;
                mAnimator.SetTrigger("Attack");
            }
            else if (distanceToCharacter > attackDistance && isAttacking)
            {
                isAttacking = false;
                attackTimer = 0f;
                mAnimator.SetTrigger("Idle");
            }

            if (isAttacking)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= 5f)
                {
                    hasDied = true;
                    mAnimator.SetTrigger("Die");
                    otherCharacter.GetComponent<Animator>().SetTrigger("Die");
                }
            }
        }
        else if (distanceToCharacter > attackDistance)
        {
            hasDied = false;
            mAnimator.SetTrigger("Idle");
            otherCharacter.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
