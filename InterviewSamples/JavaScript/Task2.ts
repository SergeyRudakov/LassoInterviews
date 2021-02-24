/*
 We need to get  treenode  values on specific level
 1. Can we change TreeNode interface to use string as value? (use generics)
 
    
*/
interface TreeNode {
    value: number;
    children: TreeNode[];
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

function getValues(tree: TreeNode, level: number): number[] {

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

    return null;
}

const duplicates = getDuplicates('faaaabbcpppefpxh');

console.log(duplicates);

