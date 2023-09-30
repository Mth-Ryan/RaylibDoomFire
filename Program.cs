using Raylib_cs;

const int pixelSize = 10;
const int fireWidth = 60;
const int fireHeight = 50;
const int maxFireIntensity = 35;
const int overflowPixelIndex = fireWidth * fireHeight;

const int screenWidth = fireWidth * pixelSize;
const int screenHeight = fireHeight * pixelSize;

int[] pixelsTable = new int[fireWidth * fireHeight];

Random random = new Random();

Color[] Palette = new Color[37]
{
    new Color(7, 7, 7, 255),
    new Color(31, 7, 7, 255),
    new Color(47, 15, 7, 255),
    new Color(71, 15, 7, 255),
    new Color(87, 23, 7, 255),
    new Color(103, 31, 7, 255),
    new Color(119, 31, 7, 255),
    new Color(143, 39, 7, 255),
    new Color(159, 47, 7, 255),
    new Color(175, 63, 7, 255),
    new Color(191, 71, 7, 255),
    new Color(199, 71, 7, 255),
    new Color(223, 79, 7, 255),
    new Color(223, 87, 7, 255),
    new Color(223, 87, 7, 255),
    new Color(215, 95, 7, 255),
    new Color(215, 95, 7, 255),
    new Color(215, 103, 15, 255),
    new Color(207, 111, 15, 255),
    new Color(207, 119, 15, 255),
    new Color(207, 127, 15, 255),
    new Color(207, 135, 23, 255),
    new Color(199, 135, 23, 255),
    new Color(199, 143, 23, 255),
    new Color(199, 151, 31, 255),
    new Color(191, 159, 31, 255),
    new Color(191, 159, 31, 255),
    new Color(191, 167, 39, 255),
    new Color(191, 167, 39, 255),
    new Color(191, 175, 47, 255),
    new Color(183, 175, 47, 255),
    new Color(183, 183, 47, 255),
    new Color(183, 183, 55, 255),
    new Color(207, 207, 111, 255),
    new Color(223, 223, 159, 255),
    new Color(239, 239, 199, 255),
    new Color(255, 255, 255, 255)
};

void CreateFireSource()
{
    for (var column = 0; column < fireWidth; column++)
    {
        var pixelIndex = (overflowPixelIndex - fireWidth) + column;

        pixelsTable[pixelIndex] = maxFireIntensity;
    }
}

void CalculateFirePropagation()
{
    for (var column = 0; column < fireWidth; column++)
    {
        for (var row = 0; row < fireHeight; row++)
        {
            var pixelIndex = (fireWidth * row) + column;
            UpdateFireIntensityPerPixel(pixelIndex);
        }
    }
}

void UpdateFireIntensityPerPixel(int currentPixelIndex)
{
    var bellowPixelIndex = currentPixelIndex + fireWidth;

    if (bellowPixelIndex >= overflowPixelIndex)
        return;

    var intensityDecay = (int)Math.Floor(random.NextDouble() * 3);
    var currentBellowPixelIntensity = pixelsTable[bellowPixelIndex];
    var newFireIntensity = currentBellowPixelIntensity - intensityDecay >= 0 ?
        currentBellowPixelIntensity - intensityDecay :
        0;

    var neigborPixelIndex = currentPixelIndex - intensityDecay;
    if (neigborPixelIndex >= 0 && neigborPixelIndex < overflowPixelIndex)
    {
        pixelsTable[neigborPixelIndex] = newFireIntensity;
    }
}

void RenderFire()
{
    Raylib.ClearBackground(Palette[0]);
    for (var row = 0; row < fireHeight; row++)
    {
        for (var column = 0; column < fireWidth; column++)
        {
            var pixelIndex = (fireWidth * row) + column;
            var fireIntensity = pixelsTable[pixelIndex];

            Raylib.DrawRectangle(
                column * pixelSize,
                row * pixelSize,
                pixelSize,
                pixelSize,
                Palette[fireIntensity]);
        }
    }
}

Raylib.InitWindow(screenWidth, screenHeight, "Raylib Doom Fire in C#");
Raylib.SetTargetFPS(60);

while (!Raylib.WindowShouldClose())
{
    CreateFireSource();
    Raylib.BeginDrawing();
    {
        CalculateFirePropagation();
        RenderFire();
    }
    Raylib.EndDrawing();
}
