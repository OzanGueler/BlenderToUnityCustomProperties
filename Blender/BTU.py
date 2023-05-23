import bpy
from bpy.props import (StringProperty, PointerProperty, BoolProperty)
import enum

class CreateLODs(bpy.types.Operator):
    bl_idname = "object.create_lods"
    bl_label = "Create LODs"
    bl_options = {"REGISTER", "UNDO"}
    
    def execute(self, context):
        if bpy.context.active_object == None:
            return {"FINISHED"}
        
        objects = []
        decimateRatio = 0.8
        p = 1
        for i in range(2):
            new_obj = bpy.context.active_object.copy()
            bpy.context.collection.objects.link(new_obj)
            new_obj.modifiers.new("LOD"+str(p), "DECIMATE")
            new_obj.modifiers["LOD"+str(p)].ratio = decimateRatio
            decimateRatio = decimateRatio/2
            
            if bpy.context.active_object.name[-4:-1] == "LOD":
                new_obj.name = new_obj.name[:-9]+"_LOD"+str(p)
            else:
                new_obj.name = new_obj.name[:-4]+"_LOD"+str(p)
            
            p = p + 1
        
        if bpy.context.active_object.name[-4:-1] != "LOD":
            bpy.context.active_object.name = bpy.context.active_object.name+"_LOD0"
        
        return {"FINISHED"}

class PanelDefaults:
    bl_space_type = "VIEW_3D"
    bl_region_type = "UI"
    bl_category = "Tools"
    bl_context = "objectmode"
    bl_options = {"DEFAULT_CLOSED"}

class LinkPrefab(bpy.types.Operator):
    bl_idname = "object.link_prefab"
    bl_label = "Link Prefab"
    bl_options = {"REGISTER"}
    
    def execute(self, context):
        context.active_object["Prefab"] = str(context.scene.prefabPath.get("prefab_path"))
        
        return {"FINISHED"}

class MyProperties(bpy.types.PropertyGroup):
    prefab_path : StringProperty(
        name="Prefab File",
        description="Path to Prefab File",
        subtype="FILE_PATH")
    my_bool : BoolProperty(name="Enable or Disable", description="A simple bool property", default=False)

class BTUSidebar(PanelDefaults, bpy.types.Panel):
    bl_idname = "OBJECT_PT_btu"
    bl_label = "BTU"
    
    def draw(self, context):
        self.layout.label(text="Blender to Unity Tools")

class OptimizationSidebar(PanelDefaults, bpy.types.Panel):
    bl_parent_id = "OBJECT_PT_btu"
    bl_idname = "OBJECT_PT_btu_optimization"
    bl_label = "Optimization"
    
    def draw(self, context):
        self.layout.operator(CreateLODs.bl_idname, text="Create LODs", icon="CONSOLE")

class PrefabSidebar(PanelDefaults, bpy.types.Panel):
    bl_parent_id = "OBJECT_PT_btu"
    bl_idname = "OBJECT_PT_btu_prefab"
    bl_label = "Prefab Links"
    
    def draw(self, context):
        self.layout.operator(LinkPrefab.bl_idname, text="Link Prefab", icon="CONSOLE")
        self.layout.prop(context.scene.prefabPath, "prefab_path")

class SetFlagsSidebar(PanelDefaults, bpy.types.Panel):
    bl_parent_id = "OBJECT_PT_btu"
    bl_idname = "OBJECT_PT_btu_flags"
    bl_label = "Set Flags"
    
    def draw(self, context):
        self.layout.prop(context.scene.GI, "my_bool", text="CONTRIBUTE GI")
        self.layout.prop(context.scene.OCCLUDER, "my_bool", text="OCCLUDER STATIC")
        self.layout.prop(context.scene.BATCHING, "my_bool", text="BATCHING STATIC")
        self.layout.prop(context.scene.NAVIGATION, "my_bool", text="NAVIGATION STATIC")
        self.layout.prop(context.scene.OCCLUDEE, "my_bool", text="OCCLUDEE STATIC")
        self.layout.prop(context.scene.OFFMESHLINK, "my_bool", text="OFFMESHLINK")
        self.layout.prop(context.scene.REFLECTIONPROBE, "my_bool", text="REFLECTIONPROBE STATIC")
        self.layout.operator(SetFlags.bl_idname, text="Save Flags", icon="CONSOLE")

class SetFlags(bpy.types.Operator):
    bl_idname = "object.set_flags"
    bl_label = "Set Flags"
    bl_options = {"REGISTER"}
    
    def execute(self, context):
        flags = 0
        if context.scene.GI.my_bool:
            flags += 1 << 0
        if context.scene.OCCLUDER.my_bool:
            flags += 1 << 1
        if context.scene.BATCHING.my_bool:
            flags += 1 << 2
        if context.scene.NAVIGATION.my_bool:
            flags += 1 << 3
        if context.scene.OCCLUDEE.my_bool:
            flags += 1 << 4
        if context.scene.OFFMESHLINK.my_bool:
            flags += 1 << 5
        if context.scene.REFLECTIONPROBE.my_bool:
            flags += 1 << 6
        context.active_object["StaticFlags"] = str(flags)
        
        return {"FINISHED"}

def register():
    classes = [CreateLODs, LinkPrefab, SetFlags, BTUSidebar, MyProperties, OptimizationSidebar, PrefabSidebar, SetFlagsSidebar]
    
    for iClass in classes:
        bpy.utils.register_class(iClass)
    
    bpy.types.Scene.prefabPath = PointerProperty(type=MyProperties)
    bpy.types.Scene.GI = PointerProperty(type=MyProperties)
    bpy.types.Scene.OCCLUDER = PointerProperty(type=MyProperties)
    bpy.types.Scene.BATCHING = PointerProperty(type=MyProperties)
    bpy.types.Scene.NAVIGATION = PointerProperty(type=MyProperties)
    bpy.types.Scene.OCCLUDEE = PointerProperty(type=MyProperties)
    bpy.types.Scene.OFFMESHLINK = PointerProperty(type=MyProperties)
    bpy.types.Scene.REFLECTIONPROBE = PointerProperty(type=MyProperties)
    
def unregister():
    for iClass in classes:
        bpy.utils.unregister_class(iClass)
    
    del bpy.types.Scene.prefabPath
    del bpy.types.Scene.GI

if __name__ == "__main__":
    register()