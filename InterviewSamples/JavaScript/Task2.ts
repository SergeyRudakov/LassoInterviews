/*
 We need to get  treenode  values on specific level
 1. Can we change TreeNode interface to use string as value? (use generics)
 
    
*/
interface TreeNode {
    value: Number;
    children: TreeNode[];
}

const tree = {
    value: 5, children:
        [{
            value: 7, children:
                    [{ value: 10, children: null }, { value: 13, children: null }]
        },
         {
                value: 25, children:
                 [{ value: 9, children: null },
                  { value: -5, children: [{ value: 100, children: null }] }]
         }
        ]
};

function getValues(tree: TreeNode, level: Number): Number[] {

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


