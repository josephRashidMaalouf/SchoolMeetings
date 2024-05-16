# Endpoints

## Custom Authentication

| path         | method | req auth | parameters | request      | response | response codes | description                        |
| ------------ | ------ | -------- | ---------- | ------------ | -------- | -------------- | ---------------------------------- |
| /user/logout | post   | yes      | None       | empty object |          | 200            | Logs out user by destroying cookie |
| /user/roles  | get    | yes      | none       | none         |          | 200            | Returns a list of userroles        |

## Meetings

| path                              | method | req auth | parameters                                     | request        | response  | response codes | description                                                                              |
| --------------------------------- | ------ | -------- | ---------------------------------------------- | -------------- | --------- | -------------- | ---------------------------------------------------------------------------------------- |
| /meetings/unbooked/{teacherEmail} | GET    | no       | string teacherEmail(path)                      |                | meeting[] | 200            | returns a list of all unbooked meetings for a specific teacher                           |
| /meetings/unbooked/{teacherEmail} | GET    | yes      | string teacherEmail(path)                      |                | meeting[] | 200            | returns a list of all booked meetings for a specific teacher                             |
| /meetings/unbooked/{teacherEmail} | GET    | yes      | string teacherEmail(path)                      |                | meeting[] | 200            | returns a list of all meetings for a specific teacher                                    |
| /meetings/all                     | GET    | yes      | string teacherEmail(query), string date(query) |                | meeting[] | 200            | returns a list of all meetings for a specific teacher for a specific date                |
| /meetings/{id}                    | GET    | yes      | string id (path)                               |                | meeting   | 200, 404       | returns a meeting with matching id                                                       |
| /meetings/{id}                    | DELETE | yes      | string id (path)                               |                | bool      | 200, 404       | deletes a meeting with matching id, returns a bool indicating deletion success           |
| /meetings/book                    | PUT    | no       |                                                | bookMeetingDto | meeting   | 200, 404, 422  | changes the IsBooked property of a specific meeting to true, returns true if successful  |
| /meetings/cancel/{meetingId}      | PUT    | no       |                                                | meeting        | meeting   | 200, 404, 422  | changes the IsBooked property of a specific meeting to false, returns true if successful |
| /meetings                         | PUT    | yes      |                                                | meeting        | bool      | 200, 422       | updates a specific property                                                              |
| /meetings                         | POST   | yes      |                                                | meeting        | meeting   | 200            | adds a meeting to the database and returns the newly added meeting with its id           |
