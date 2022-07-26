/*
 We need to get  treenode  values on specific level
 1. Can we change TreeNode interface to use string as value? (use generics)
 
    
*/
interface TreeNode<T>{
    value: T;
    children: TreeNode<T>[] | null;
}

const tree = {
    value: 5, children:
        [{
            /*Level 2*/
            value: 7, children:
                 [{ /*Level 3*/ value: 10, children: null }, { value: 13, children: null }]
        },
         {
                value: 25, children:
                 [{ value: 9, children: null },
                     { value: -5, children: [{ /*Level 4*/ value: 100, children: null }] }]
         }
        ]
};

function getValues<T>(tree: TreeNode<T>, level: number): number[] 
{
    return [];
}

const result = getValues(tree, 3);

console.log(result);


/*
 Task 2:
 given a string return only duplicated letters
 faaaabbcpppefpxh -> abpf
 */

function getDuplicates(input: string): string {
    let result = "";
    let elem = {};
    
    //count visited
    for (let char of input) {
        if (elem[char])
            elem[char]++;
        else
            elem[char] = 1;
    }
    
    //find all visited
    for (let key in elem)
    {
        if (elem[key] > 1)
            elem += key;
    }
    return result.split('').join('');
}

const duplicates = getDuplicates('faaaabbcpppefpxh');

console.log(duplicates);

