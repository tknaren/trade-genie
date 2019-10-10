var logger = require('../NodeApp/logger');


function sayHello(name){
    console.log('Hello ' + name);
}

sayHello('Naren');


// setTimeout(); //call a function after a delay, part of the Javascript
// clearTimeout(); //

// setInterval(); // repeatedly call a function after a delay
// clearInterval();// to stop the functions be called repeatedly

// create building blocks or modules. so the functions will not override the other
// every file in a node application is considered as a module

console.log(logger); // this is display the methods and functions that are present as part of that module.
