window.setTitle = (title) => {
    document.title = title + " - Blazor";
}

window.SetFocusToElement = (element) => {
    element.focus();
};

window.PlayAudio = (elementName) => {
    document.getElementById(elementName).play();
}

window.PauseAudio = (elementName) => {
    document.getElementById(elementName).pause();
}

window.WriteCookie = (name, value, days) => {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

window.ReadCookie = (cname) => {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return 0;
}


window.JsFunctions = {
    addKeyboardEventListener: function () {
        let serializedEvent = (e) => {
            return {
                key: e.key,
                code: e.keyCode.toString(),
                location: e.location,
                repeat: e.repeat,
                ctrlKey: e.ctrlKey,
                shiftKey: e.shiftKey,
                altKey: e.altKey,
                metaKey: e.metaKey,
                type: e.type
            };
        }
        window.document.addEventListener('keydown', (e) => {
            DotNet.invokeMethodAsync('BlazorApp', 'JsKeyDown', serializedEvent(e))
        })
    },
}

console.log("utilities.js loaded");

let app = null;

window.runpixi = async () => {
    app = new PIXI.Application(
        {
            width: 960,
            height: 720,
            backgroundColor: 0xAAAAAA
        }
    );
    // The application will create a canvas element for you that you
    // can then insert into the DOM
    document.querySelector('.canvas-container').appendChild(app.view);
    
    app.loader.add('font/ps2p.fnt'); //  bitmap - quite fast
    // app.loader.add('font/PressStart2P/PressStart2P.ttf'); // regular - very slow
    
    return new Promise((resolve, reject) => {
        console.log("app.loader loading...");
        app.loader.load();
        
        app.loader.onComplete.add(() => {
            console.log("app.loader resolve");
            resolve();
        })
        app.loader.onError.add(() => {
            console.log("app.loader onError");
            reject();
        })
    });
}

function doNothing(drawArea) {

}

async function printDate(dotNetHelper) {
    let date = await dotNetHelper.invokeMethodAsync('GetDate');
    console.log(date);
}

function rgbToHex(r, g, b) {
    return "0x" + [r, g, b].map(x => {
        const hex = x.toString(16);
        return hex.length === 1 ? "0" + hex : hex;
    }).join('');
}

async function getTime() {
    return Date.now().toString();
}

function PixiMainWithIsJson(drawArea, isFirstRun) {
    pixiMain(drawArea, isFirstRun, true, false)
}

function PixiMainWithIsJsonIsUnmarshalled(drawArea, isFirstRun) {
    pixiMain(drawArea, isFirstRun, true, true)
}


function pixiMain(drawArea, isFirstRun, isJson, isUnmarshalled) {
    // The application will create a renderer using WebGL, if possible,
    // with a fallback to a canvas render.
    

    drawArea = isUnmarshalled ? BINDING.conv_string(drawArea) : drawArea;       // Convert the handle to a JS string
    drawArea = isJson ? JSON.parse(drawArea) : isJson;
    

    if (isFirstRun) {
        console.log("in pixiMain");
        console.log(drawArea);
        console.log(app.stage.children);
    }
    
    /*
    let start = performance.now();
    let children = app.stage.removeChildren();  // removes previous stage effectively clearing the screen
    for (const child of children) {
        child.destroy(true);
    }
    
     
    console.log(`destroy took ${performance.now() - start} ms`)
    
     */
    
    let height = 40;
    let width = 100;
    for (let y = 0; y < height; y++) {
        for (let x = 0; x < width; x++) {
            let tile = drawArea[y * width + x];
            if (! isFirstRun) {
                app.stage.children[y * width + x].text = tile.Char;
                app.stage.children[y * width + x].tint = rgbToHex(tile.ColorRGBr, tile.ColorRGBg, tile.ColorRGBb);
            } else {
                let scale = 1;
                let fontSize = 8;
                let textChar
                if (true) {

                    textChar = new PIXI.BitmapText(tile.Char, {
                        fontName: "Press Start 2P",
                        fontSize: fontSize,
                        align: "right"
                    });
                    textChar.tint = rgbToHex(tile.ColorRGBr, tile.ColorRGBg, tile.ColorRGBb);
                } else {
                
                    textChar = new PIXI.Text(tile.Char);
                    textChar.style = new PIXI.TextStyle(
                        {
                            fill: 0x008000,
                            fontSize: fontSize,
                            fontFamily: "Press Start 2P"
                        }
                    )
                }
                
                 
                textChar.x = x * fontSize * scale;
                textChar.y = y * fontSize * scale;
                
                textChar.scale.x = scale;
                textChar.scale.y = scale;
                app.stage.addChild(textChar);
            }
        }
    }

    /* 
    // load the texture we need
    app.loader.add('bunny', 'images/bunny.png').load((loader, resources) => {
        // This creates a texture from a 'bunny.png' image
        const bunny = new PIXI.Sprite(resources.bunny.texture);

        // Setup the position of the bunny
        bunny.x = app.renderer.width / 2;
        bunny.y = app.renderer.height / 2;
        bunny.scale.x = 0.3;
        bunny.scale.y = 0.3;
        // Rotate around the center
        bunny.anchor.x = 0.5;
        bunny.anchor.y = 0.5;

        // Add the bunny to the scene we are building

        // -----

        // -------


        app.stage.addChild(bunny);

        // Listen for frame updates
        app.ticker.add(() => {
            // each frame we spin the bunny around a bit
            bunny.rotation += 0.01;
        });
        

    });
    
    
     */

}