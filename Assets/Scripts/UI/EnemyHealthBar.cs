using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(SpriteRenderer))]
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color _blinkingColor;

    private Enemy enemy;
    private SpriteRenderer enemySpriteRenderer;
    private MaterialPropertyBlock matBlock;
    private Camera mainCamera;
    private Color startColor;

    private Coroutine blinkingCoroutine;

    private void Awake()
    {
        matBlock = new MaterialPropertyBlock();
        enemy = GetComponent<Enemy>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();

        startColor = enemySpriteRenderer.color;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        enemy.EnemyChangedDamage += OnUpdateBar;

        enemySpriteRenderer.color = startColor;
    }

    private void OnDisable()
    {
        enemy.EnemyChangedDamage -= OnUpdateBar;

        blinkingCoroutine = null;
    }

    private void FixedUpdate()
    {
        AlignCamera();
    }

    private void OnUpdateBar()
    {
        _meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", (float)enemy.Health / enemy.StartHealth);
        _meshRenderer.SetPropertyBlock(matBlock);

        if (blinkingCoroutine == null) 
        {
            blinkingCoroutine = StartCoroutine(Blinking());
        }
        else
        {
            StopCoroutine(blinkingCoroutine);
            blinkingCoroutine = StartCoroutine(Blinking());
        }
    }

    private IEnumerator Blinking()
    {
        enemySpriteRenderer.color = _blinkingColor;
        yield return new WaitForSeconds(0.15f);
        enemySpriteRenderer.color = startColor;

        blinkingCoroutine = null;
    }

    private void AlignCamera()
    {
        _meshRenderer.transform.rotation = mainCamera.transform.rotation;
    }
}
