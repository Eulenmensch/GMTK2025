﻿using System;
using UnityEngine;

namespace Obi{

	public class ObiSphereShapeTracker : ObiShapeTracker
	{

		public ObiSphereShapeTracker(ObiCollider source, SphereCollider collider)
        {
            this.source = source;
			this.collider = collider;
		}	

		public override void UpdateIfNeeded()
        {
			SphereCollider sphere = collider as SphereCollider;

            // retrieve collision world and index:
            var world = ObiColliderWorld.GetInstance();
            int index = source.Handle.index;

            // update collider:
            var shape = world.colliderShapes[index];
            shape.type = ColliderShape.ShapeType.Sphere;
            shape.filter = source.Filter;
            shape.SetSign(source.Inverted);
            shape.isTrigger = sphere.isTrigger;
            shape.rigidbodyIndex = source.Rigidbody != null ? source.Rigidbody.Handle.index : -1;
            shape.materialIndex = source.CollisionMaterial != null ? source.CollisionMaterial.handle.index : -1;
            shape.forceZoneIndex = source.ForceZone != null ? source.ForceZone.Handle.index : -1;
            shape.contactOffset = source.Thickness;
            shape.center = sphere.center;
            shape.size = Vector3.one * sphere.radius;
            world.colliderShapes[index] = shape;

            // update bounds:
            var aabb = world.colliderAabbs[index];
            aabb.FromBounds(sphere.bounds, shape.contactOffset);
            world.colliderAabbs[index] = aabb;

            // update transform:
            var trfm = world.colliderTransforms[index];
            trfm.FromTransform3D(sphere.transform, source.Rigidbody as ObiRigidbody);
            world.colliderTransforms[index] = trfm;
		}

	}
}

