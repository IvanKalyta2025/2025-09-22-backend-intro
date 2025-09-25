console.log("JavaScript loaded")

// Setup form logic
const todoForm = document.querySelector("#todo-input-form")
todoForm.addEventListener("submit", (event) => {
  event.preventDefault() // Don't refresh
  
  // Read off current values from inputs
  const descriptionElement = todoForm.querySelector("#description")
  const description = descriptionElement.value

  // Package into a nice object
  const formPayload = {
    Description: description,
  }
  console.log(formPayload)

  // Send to server
  fetch("http://localhost:5282/todo", {
    method: "POST",
    body: JSON.stringify(formPayload),
    headers: {
      "Content-Type": "application/json"
    }
  })
})

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