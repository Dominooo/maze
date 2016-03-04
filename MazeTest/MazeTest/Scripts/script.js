// TEXT OBJECT WITH THESE PROPERTIES ---   KEY:[X,Y,STATUS]
var mazeObject = {
    //a0:[0,0,1],
};

function initiate() {
    genMaze();
    draw();
    moveFromStart();

}

var wallArray = ['http://imgur.com/lOPDudx', 'http://imgur.com/vxhPKEW', 'http://imgur.com/fJPl46S'];
var floorArray = ['http://imgur.com/01rh3Pg', 'http://imgur.com/3Ay38Pi', 'http://imgur.com/rT0kORZ'];


//GENERATE A FAKE MAZE TO TEST WITH
function genMaze() {
    //var alphabet = ['a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','aa','bb','cc','dd','ee','ff','gg','hh','ii','jj','kk','ll','mm','nn','oo','pp','qq','rr','ss','tt','aaa','bbb','ccc','ddd','eee','fff','ggg','hhh','iii','jjj','kkk','lll','mmm','nnn','ooo','ppp','qqq','rrr','sss','ttt','aaaa','bbbb','cccc','dddd','eeee','ffff','gggg','hhhh','iiii','jjjj','kkkk','llll','mmmm','nnnn','oooo','pppp','qqqq','rrrr','ssss','tttt'];
    var alphabet2 = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't'];
    var mazeSize = 20;


    //LOOP THROUGH EACH LETTER (AKA Y COORDINATE)
    for (var j = 0; j < alphabet2.length; j++) {
        var currentLetter = alphabet2[j];

        //LOOP THROUGH EACH NUMBER (AKA X COORDINATE)
        for (var k = 0; k < mazeSize; k++) {
            var keyName = currentLetter + k;
            var status;

            //RANDOMLY DECIDE IF A TILE IS WALKWAY OR WALL
            var coinToss = Math.random();
            if (coinToss < 0.5) {
                status = 1;
            } else {
                status = 0;
            }

            // IF IT'S AN EDGE IT'S SOLID
            if (k == mazeSize - 1) {
                status = 1;
            }
            if (j == mazeSize - 1) {
                status = 1;
            }
            if (k === 0) {
                status = 1;
            }
            if (j === 0) {
                status = 1;
            }

            // ASSIGN THE OBJECT
            mazeObject[keyName] = [j, k, status];

        }// end inner loop x

        //PLACE A STARTING TILE
        var randomSide = Math.random();
        if (randomSide > 0.5) {
            startSide = 0;
        } else {
            startSide = mazeSize - 1;
        }

        var randomSpace = Math.floor(Math.random() * (mazeSize - 1)) + 1;
        var finalPlacement = Math.random();
        if (finalPlacement > 0.5) {
            mazeObject.start = [startSide, randomSpace, 0];
        } else {
            mazeObject.start = [randomSpace, startSide, 0];
        }

        console.log(mazeObject.start);

    }// end outer loop yˆ

}

function moveFromStart() {
    if (mazeObject.start === 0) {

    } else {

    }

}



// DECLARE CANVAS VARIABLES
var canvas = document.getElementById('tutorial');
var tileSize = 32;
var ctx = {};
var img;

// DRAW THE MAZE
function draw() {
    //genMaze();
    console.log(mazeObject);

    var canvas = document.getElementById("canvas");
    if (canvas.getContext) {

        // LOOP THROUGH OUR OBJECT AND DRAW THE TILES
        for (var tile in mazeObject) {
            if (mazeObject.hasOwnProperty(tile)) {

                //DECLARE NEW VAR FOR CANVAS TO DRAW
                ctx.tile = canvas.getContext("2d");

                // TEMPORARILY GET THE VALUE FOR CURRENT TILE
                var tempTileArray = mazeObject[tile];
                //console.log('***********');

                var randomTileArt = Math.floor(Math.random() * (2 - 0 + 1)) + 0;

                //  WHAT COLOR SHOULD THE TILE BE?
                if (tempTileArray[2] === 1) {
                    img = document.getElementById("floor" + randomTileArt);
                } else {
                    img = document.getElementById("wall" + randomTileArt);
                }

                // WHERE SHOULD THE TILE GO?
                var xcoord = (tempTileArray[0] * tileSize);
                var ycoord = (tempTileArray[1] * tileSize);

                // ACTUALLY DRAW IT
                ctx.tile.drawImage(img, xcoord, ycoord);

            }
        }

        //DRAW THE START TILE
        ctx.start = canvas.getContext("2d");
        ctx.start.fillStyle = '#bada55';
        ctx.start.fillRect(mazeObject.start[0] * tileSize, mazeObject.start[1] * tileSize, tileSize, tileSize);

    }
}