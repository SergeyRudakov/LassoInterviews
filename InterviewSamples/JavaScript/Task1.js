//TASK 1:
//transfrom people into states array 
const people = [{ name: "John Doe", age: 40, state: "CA" }, { name: "Jane Doe", age: 25, state: "NY" }, { name: "Tom Cruise", age: 55, state: "CA" }];

//states = [{name:"CA",averageAge: 47.5, people: ["John Doe", "Tom Cruise"] },{name:"NY",averageAge: 25, people: ["Jane Doe"] }];
function getStates(people) {
    
}


console.log(people);



//TASK 2
//Write counter functions that can expose increment and decrement functions

let Counter = function (number){
    this.number = number;
    
    this.increment = function (){
        return ++this.number;
    }
    
    this.decrement = function (){
        return --this.number;
    }
}

 let counter1 = new Counter(100);
 let counter2 = new Counter(200);
 
 counter1.increment(); //-> 101
 counter1.increment(); //-> 102
 counter1.decrement(); //-> 101
 counter2.increment(); //-> 201
 counter1.increment(); //-> 102



