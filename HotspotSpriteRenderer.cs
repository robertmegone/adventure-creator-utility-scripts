using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add a sprite over hotspots when a key is pressed, this script should be added to the persistent engine.

public class HotspotSpriteRenderer : MonoBehaviour
{
    public Sprite sprite;
    public float spriteDuration = 3f;
    public KeyCode triggerKey = KeyCode.Space;
    public List<AC.Hotspot> blacklistedHotspots;

    private List<SpriteRenderer> activeSprites;
    private bool isFadingOut;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        activeSprites = new List<SpriteRenderer>();
        isFadingOut = false;
    }

    void Update()
    {
        if (!isFadingOut && Input.GetKeyDown(triggerKey))
        {
            // Prevent the player from triggering another fade out while one is already in progress
            isFadingOut = true;

            // Clean up any active sprites
            foreach (var sprite in activeSprites)
            {
                Destroy(sprite.gameObject);
            }
            activeSprites.Clear();

            // Get all hotspots in the scene
            AC.Hotspot[] hotspots = FindObjectsOfType<AC.Hotspot>();

            // Create a sprite for each hotspot that isn't blacklisted
            foreach (var hotspot in hotspots)
            {
                if (!blacklistedHotspots.Contains(hotspot) && hotspot.IsOn())
                {
                    var spriteRenderer = CreateSprite(hotspot.transform.position);
                    activeSprites.Add(spriteRenderer);
                    StartCoroutine(FadeOutSprite(spriteRenderer));
                }
            }
        }
    }

    SpriteRenderer CreateSprite(Vector3 position)
    {
        var spriteObject = new GameObject("HotspotSprite");
        var spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerName = "Inventory"; // Set the sorting layer to "Inventory"
        spriteRenderer.sortingOrder = 2000; // Set the sorting order to a very large value
        spriteObject.transform.position = position;
        spriteObject.transform.localScale *= 0.02f;
        return spriteRenderer;
    }

    IEnumerator FadeOutSprite(SpriteRenderer spriteRenderer)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / spriteDuration;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f - t);
            yield return null;
        }
        Destroy(spriteRenderer.gameObject);
        activeSprites.Remove(spriteRenderer);

        // Allow the player to trigger another fade out once all sprites have faded out
        if (activeSprites.Count == 0)
        {
            isFadingOut = false;
        }
    }
}
