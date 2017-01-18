var LoadInitialPage = React.createClass({

    getInitialState: function () {
        return {
            todos: []
        };
    },
    componentDidMount: function () {
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            success: function (data) {
                this.setState({ todos: data.data });
            }.bind(this),
            error: function (xhr, status, err) {
            }.bind(this)
        });
    },
    render: function () {
        return (
            <ToDoList todos={this.state.todos} />
        );
    }
});

//This is to bind the list of data
var ToDoList = React.createClass({
    render: function () {
        $('#itemcount').html(this.props.todos.length);
        let todos = Object.keys(this.props.todos).map(todo_id => {
            let todo = this.props.todos[todo_id];
            return (
                <ToDoItem todo={todo} />
         );
        });
        return (

<div className="ToDoTbl">
    {todos}
</div>

        );

    }
});

/*$$Sms_React_Script_Step1$$*/

/*$$Email_React_Script_Step1$$*/


//This is to edit, update and delete of todo item
var ToDoItem = React.createClass({
    getInitialState: function () {
        return {
            editing: false,
            Name: this.props.todo.Name
        };
    },
    _onChange: function (event) {
        this.setState({
            Name: event.target.value
        });
    },
    toggleEdit: function () {
        let editstate = !this.state.editing;
        this.setState({ editing: editstate, Name: this.props.todo.Name });
    },
    deleteToDo: function () {

        $.ajax({
            type: "post",
            url: "/todo/delete/" + this.props.todo.Id,
            async: false
        }).done(function (data) {
            $('#ToDoTbl').html("");
            ReactDOM.render(<LoadInitialPage url="/ToDo/GetAll" />, document.getElementById('ToDoTbl'))
        }).fail(function (err) {

        });
    },
    updateToDo: function () {
        var self = this;
        $.ajax({
            type: "post",
            url: "/todo/update/",
            async: false,
            data: { "Id": this.props.todo.Id, "Name": this.state.Name }
        }).done(function (data) {
            self.props.todo.Name = self.state.Name;
            let editstate = !self.state.editing;
            self.setState({ editing: editstate });

        }).fail(function (err) {
        });
    },
    /*$$Sms_React_Script_Step2$$*/
  
    /*$$Email_React_Script_Step2$$*/
    
    render: function () {
        var todo = this.props.todo;
        return (
                <div className="clearfix ToDoRow">
                    { !this.state.editing ?
                    <div className="displayContainer">
                         <label className="fleft tdhead"><b>{todo.Name}</b></label>
                         <div className="BforEdit">
                           <a href="javascript:void(0);" onClick={this.toggleEdit.bind(this)} className="tdIcons">
                               <img id={"icon1 " + todo.Name} src="/Images/Edit.png" alt="Edit.png" />
                           </a>
                           <a href="javascript:void(0);" onClick={this.toggleEdit.bind(this)} className="tdIconsText">
                               <h5><span className="borderleft" id={"icon5 " + todo.Name}>Edit</span></h5>
                           </a>
                           <form>
                              <input id="Id" name="Id" type="hidden" value={todo.Id} />
                              <a href="javascript:void(0);" onClick={this.deleteToDo.bind(this)} className="tdIcons">
                                 <img id={"icon2 " + todo.Name} src="/Images/Delete.png" alt="Delete.png" />
                              </a>
                              <a href="javascript:void(0);" onClick={this.deleteToDo.bind(this)} className="tdIconsText toDoDeleteIcon" id={"icon6 " + todo.Name}>
                                  <h5>Delete</h5>
                              </a>
                           </form>
                             {/*Notification_React_Render_step1*/}
                             
                             {/*Sms_React_Render*/}

                             {/*Email_React_Render*/}    
                               
                             {/*Notification_React_Render_step2*/}
                           </div>
                    </div>

                     :

            <div className="clearfix AfterEdit editContainer">
                  <input className="fleft inputcls" type="text" name="Name" value={this.state.Name} onChange={this._onChange.bind(this)} />
                  <a href="javascript:void(0);" onClick={this.updateToDo.bind(this)} className="tdIcons">
                     <img id={"icon3 " + todo.Name} src="/Images/save.png" alt="save.png" />
                  </a>
                  <a href="javascript:void(0);" onClick={this.updateToDo.bind(this)} className="tdIcons">
                     <h5><span id={"icon7 " + todo.Name}>Save</span></h5>
                  </a>
                 <a href="javascript:void(0);" onClick={this.toggleEdit.bind(this)} className="tdIcons">
                    <img id={"icon4 " + todo.Name} src="/Images/close.png" alt="close.png" />
                 </a>
                 <a href="javascript:void(0);" onClick={this.toggleEdit.bind(this)} className="tdIcons" id={"icon8 " + todo.Name}>
                     <h5>Cancel</h5>
                 </a>
            </div>
                    }
                </div>
      );
    }
});

var LoadToDoForm = React.createClass({
    getInitialState: function () {
        return {
            Name: ""
        };
    },
    _onChange: function (event) {
        this.setState({
            Name: event.target.value
        });
    },
    createToDo: function () {
        var name = this.state.Name
        if ($.trim(name) !== "") {
            var postData = { 'Name': name };
            //Ajax request to create a new task
            $.ajax({
                url: '/todo/create',
                type: 'POST',
                data: postData,
                async: false
            }).done(function (result) {
                $('#ToDoTbl').html("");
                $('#todo-form').html("");
                ReactDOM.render(<LoadInitialPage url="/ToDo/GetAll" />, document.getElementById('ToDoTbl'));
                ReactDOM.render(<LoadToDoForm />, document.getElementById('todo-form'));
                $('#getredis').html("");
                ReactDOM.render(<LoadRedisCount />, document.getElementById('getredis'));
            }).fail(function (err) {
            });
        }
    },
    render: function () {
        return (

            <div className="form-group">
                <div className="inputdiv">
                    <input type="text" value={this.state.Name} onChange={this._onChange.bind(this)} placeholder="Add To Do" />
                    <button className="fright" onClick={this.createToDo.bind(this)}>
                        <img src="Images/AddIcon.png" alt="AddIcon.png" />
                    </button>
                </div>
            </div>


        );
    }
});

ReactDOM.render(<LoadInitialPage url="/ToDo/GetAll" />, document.getElementById('ToDoTbl'))
ReactDOM.render(<LoadToDoForm />, document.getElementById('todo-form'))


var LoadRedisCount = React.createClass({

    getInitialState: function () {
        return {
            rediscount: "0"
        };
    },
    componentDidMount: function () {
        $.ajax({
            url: "/cache/GetItemCount",
            dataType: 'json',
            success: function (response) {
                this.setState({ rediscount: response });
            }.bind(this),
            error: function (xhr, status, err) {
            }.bind(this)
        });
    },
    deleteCache: function () {
        $.ajax({
            type: "post",
            url: "/cache/DeleteCache/",
            async: false
        }).done(function (data) {
            $('#getredis').html("");
            ReactDOM.render(<LoadRedisCount />, document.getElementById('getredis'))
        }).fail(function (err) {

        });
    },
    render: function () {
        return (
            <div>
                <h2 className="headerdivh2 clearfix fleft">
                     <span className="fright hdrCount" id="rediscount">{this.state.rediscount}</span>
                </h2>
                <form>
                    <img className="fright" src="Images/editback.png" alt="editback.png" onClick={this.deleteCache.bind(this)} />
                </form>
            </div>
        );
    }
});

ReactDOM.render(<LoadRedisCount />, document.getElementById('getredis'))

/*$$Blob_React_Script$$*/



