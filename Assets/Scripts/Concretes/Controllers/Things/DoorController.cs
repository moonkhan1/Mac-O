using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DoorController : MonoBehaviour
{
   [SerializeField] private Transform _closePoint;
   private BoxCollider2D _boxCollider2D;
   private Rigidbody2D _rigidbody2D;

   private void Awake()
   {
      _boxCollider2D = GetComponent<BoxCollider2D>();
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   private void Start()
   {
      _rigidbody2D.bodyType = RigidbodyType2D.Static;
      _boxCollider2D.isTrigger = true;
   }
   private void OnValidate() 
   {
      GetReference();
   }
   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         CloseDoor();
      }

      if (other.CompareTag("Ground"))
      {
         _boxCollider2D.isTrigger = false;
      }
   }
   

   private async void CloseDoor()
   {
      _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
      await UniTask.Delay(1000);
      _rigidbody2D.bodyType = RigidbodyType2D.Static;

   }

   private void GetReference()
   {
      if(_rigidbody2D == null)
         _rigidbody2D = GetComponent<Rigidbody2D>();
   }
}
