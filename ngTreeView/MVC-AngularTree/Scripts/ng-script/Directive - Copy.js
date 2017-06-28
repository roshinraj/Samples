(function (f) {
    f.module("angularTreeview", []).directive("treeView", function ($compile) {
        return {
            restrict: "AE",
            require: "?ngModel",
            link: function (scope, element, attrs, ngCtrl) {
                var treeId = attrs.treeId;                
                var treeModel = attrs.treeModel;
                var nodeId = attrs.nodeId || 'ID';
                var nodeLabel = attrs.nodeLabel || 'Name';                
                var nodeChildren = attrs.nodeChildren || 'Children';
                var nodeClick = attrs.nodeClick ? attrs.nodeClick + '(node)' : '';                
                var template =
                    '<ul>' +
                    '<li ng-class="node.Highlight" ng-repeat="node in ' + treeModel + '">' +
                    '<i class="{{(node.' + nodeChildren + ' && node.' + nodeChildren + '.length > 0) ? (node.Collapsed ? &apos;collapsed&apos;: &apos;expanded&apos;):&apos;&apos;}}" ng-click="' + treeId + '.SelectNodeHead(node);' + nodeClick + '"></i>' +
                    '<i class="{{node.Icon ? node.Icon : &apos;folder&apos;}} {{node.' + nodeChildren + ' && node.' + nodeChildren + '.length > 0 ? &apos;&apos;: &apos;folder-margin&apos;}}"></i>' +
                    '<span ng-click="' + treeId + '.OnSelectNode(node);' + nodeClick + '">{{node.' + nodeLabel + '}}</span>' +
                    '<div class="tree-child" tree-view="true" ng-if="node.' + nodeChildren + '" ng-hide="node.Collapsed" tree-id="' + treeId + '" tree-model="node.' + nodeChildren + '" node-id="' + nodeId + '" node-label="' + nodeLabel + '" node-children="' + nodeChildren + '"></div>' +
                    '</li>' +
                    '</ul>';
                
                if (treeId && treeModel) {                    
                    if (attrs.treeView) {
                        scope[treeId] = scope[treeId] || {};

                        //if node head clicks,
                        scope[treeId].SelectNodeHead = scope[treeId].SelectNodeHead || function (selectedNode) {                            
                            selectedNode.Collapsed = !selectedNode.Collapsed;                            
                        };

                        //if node label clicks,
                        scope[treeId].OnSelectNode = scope[treeId].OnSelectNode || function (selectedNode) {

                            //remove highlight from previous node
                            if (scope[treeId].CurrentNode && scope[treeId].CurrentNode.Highlight) {
                                scope[treeId].CurrentNode.Highlight = '';
                            }

                            //set highlight to selected node
                            selectedNode.Highlight = 'selected';                            
                            scope[treeId].CurrentNode = selectedNode;                           
                        };
                    }

                    //Rendering template.
                    element.html('').append($compile(template)(scope));
                }
            }
        }
    })
})(angular);
