using System.Collections;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Combat;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.Loot;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Enemies
{
    public class Bandit : BaseEnemy
    {
        public override void ProcessHits()
        {
            var hits = _hitboxRegister.RegisterAttack();
            foreach (var hurtbox in hits)
            {
                hurtbox.RegisterHit(_defaultHitInfo);
            }
        }

        public override void OnHit(HitInfo hit)
        {
            _animator.SetTrigger("Hit");
            Health -= hit.Damage;
        }

        public override void Move(Vector2 distance)
        {
            if (_frozen) return;
            
            _rb.MovePosition(_rb.position + distance);
            
            var scaleFactor = Mathf.Abs(_spriteTransform.localScale.x);
            if (distance.x >= 0.00001f)
            {
                _spriteTransform.localScale = new Vector3(-scaleFactor, scaleFactor, scaleFactor);
            }
            else if (distance.x <= 0.00001f)
            {
                _spriteTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            }
        }

        public override void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        public override void Die()
        {
            _animator.SetBool("isKilled", true);
            StartCoroutine(Destroy());
            _enemyKilledEvent.Fire(new Dictionary<string, object>
            {
                { "enemyId", Id }
            });
        }

        // Unfortunately, because its weirdly complicated to provide animation events from parent scripts of the game object,
        // enemy scripts must be on their Graphic and not their parent object. As such, destroying the parent is important
        // to also destroy health bars/all other children.
        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(2f);
            GetComponentInParent<LootSpawner>()?.SpawnLoot();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}