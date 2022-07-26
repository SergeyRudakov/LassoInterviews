//TASK 1:
//transfrom people into states array
const people = [
    { name: 'John Doe', age: 40, state: 'CA' },
    { name: 'Jane Doe', age: 25, state: 'NY' },
    { name: 'Tom Cruise', age: 55, state: 'CA' }
];

//states = [
// {name:"CA",averageAge: 47.5, people: ["John Doe", "Tom Cruise"] },
// {name:"NY",averageAge: 25, people: ["Jane Doe"] }
// ];

//first implementation (dummy)

function getStatesDummy(people) {
    let states = [];

    for (let ind in people) {
        let person = people[ind];
        let found = states.find(x => x.name === person.state);

        if (found)
        {
            found.people.push(person.name);
            found.averageAge += person.age
        }
        else
        {
            states.push({ name: person.state, averageAge: person.age, people:[ person.name ] });
        }
    }

    for (let ind in states) {
        let state = states[ind];
        state.averageAge = state.averageAge / state.people.length;
    }

    return states;
}



//second implementation (smart)
function getStates(people) {
    const states = {}
    people.forEach(item => {
        if (!states[item.state]) {
            states[item.state] = {
                ageSum: 0,
                people: [],
            }
        }
        states[item.state].ageSum += item.age;
        states[item.state].people.push(item.name);
    })
    return Object.keys(states).map(key => {
        const state = states[key];
        return {
            name: key,
            averageAge: state.ageSum / state.people.length,
            people: state.people,
        }
    })
}

console.log(getStates(people));



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



