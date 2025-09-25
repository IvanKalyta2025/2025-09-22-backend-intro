console.log("JavaScript loaded")

// Wait for the first HTTP response
const response = await fetch("http://localhost:5282/todo")
console.log(response)

// Wait for all data and unwrap
const todoList = await response.json()
console.log(todoList)

for (const todoItem of todoList) {
  console.log(todoItem)
  const todoElement = document.createElement("li")
  todoElement.textContent = todoItem.description
  
  const todoListElement = document.querySelector("#todo-list")
  todoListElement.append(todoElement)
}